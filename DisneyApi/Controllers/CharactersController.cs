using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisneyApi.Data;
using DisneyApi.Models;
using AutoMapper;
using DisneyApi.Dto;
using Microsoft.AspNetCore.Authorization;

namespace DisneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharactersController : ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        private readonly IPeliculaOSerieRepository _peliculaOSerieRepository;
        private readonly IMapper _mapper;

        public CharactersController(IPersonajeRepository personajeRepository, IMapper mapper, IPeliculaOSerieRepository peliculaOSerieRepository)
        {
            _personajeRepository = personajeRepository;
            _mapper = mapper;
            _peliculaOSerieRepository = peliculaOSerieRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonajesDto>>> GetPersonajes([FromQuery] int? age,[FromQuery] int? idMovie, [FromQuery] string name)
        {
            return Ok(_mapper.Map<IEnumerable<PersonajesDto>>(await _personajeRepository.GetAll(age, idMovie, name)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajeDetailDto>> GetPersonaje(int id)
        {
            var personaje = _mapper.Map<PersonajeDetailDto>(await _personajeRepository.Get(id));
            if (personaje == null)
            {
                return NotFound();
            }

            return Ok(personaje);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, PersonajeCreateDto personaje)
        {
            var p = await _personajeRepository.Get(id);
            if (p == null)
            {
                return NotFound();
            }

            await _personajeRepository.Update(_mapper.Map(personaje, p));
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PersonajeDetailDto>> PostPersonaje([FromQuery]int[] peliculasOSeriesId,[FromBody]PersonajeCreateDto personaje)
        {
            var p = _mapper.Map<Personaje>(personaje);
            p.PeliculasOSeries = new List<PeliculaOSerie>();
            foreach (int id in peliculasOSeriesId)
            {
                var sOp = await _peliculaOSerieRepository.Get(id);
                if(sOp == null)
                {
                    ModelState.AddModelError(nameof(p.PeliculasOSeries), $"peliculasOSeriesId {id} does not exist");
                    return BadRequest(ModelState);
                }
                p.PeliculasOSeries.Add(sOp);
            }

            var pd =_mapper.Map<PersonajeDetailDto>(await _personajeRepository.Add(p));
            return CreatedAtAction("GetPersonaje", new { id = pd.Id }, pd);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _personajeRepository.Get(id);
            if (personaje == null)
            {
                return NotFound();
            }
            await _personajeRepository.Delete(personaje);
            return NoContent();
        }
    }
}
