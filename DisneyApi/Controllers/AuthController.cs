using DisneyApi.Dto;
using DisneyApi.Helper;
using DisneyApi.Models;
using DisneyApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor,IEmailService emailService)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthResult>> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>(){"Email already exist" }
                    });
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Email };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    await _emailService.SendWelcomeEmail(newUser.Email);
                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }

                return new JsonResult(new AuthResult()
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()
                }
                        )
                { StatusCode = 500 };
            }

            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>() { "Invalid payload" }
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] ApiUser user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>(){ "Invalid authentication request"}
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    var jwtToken = GenerateJwtToken(existingUser);

                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>(){"Invalid authentication request"}
                    });
                }
            }

            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>(){"Invalid payload"}
            });
        }


        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
