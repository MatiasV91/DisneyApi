using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task SendWelcomeEmail(string email)
        {
            var client = new SendGridClient(_configuration["EmailConfig:ApiKey"]);
            var msg = new SendGridMessage
            {
                From = new EmailAddress("DisneyApi@protonmail.com", "Disney API"),
                Subject = "Disney Api Registration",
                HtmlContent = "<h1>Welcome to Disney API</h1>"
                            + "<h2> Your registration has been successful<h2>"
                            + $"<p> Now you are able to login with your Email '{ email }' and receive a JWT Token to use the full Disney Api <p>"
            };
            msg.AddTo(email, email);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
