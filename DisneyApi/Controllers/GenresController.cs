using AutoMapper;
using DisneyApi.Data;
using DisneyApi.Dto;
using DisneyApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : ControllerBase
    {
        private readonly IGeneroRepository  _generoRepository;
        private readonly IMapper _mapper;
        private readonly IPeliculaOSerieRepository _peliculaOSerieRepository;

        public GenresController(IGeneroRepository generoRepository, IMapper mapper, IPeliculaOSerieRepository peliculaOSerieRepository)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
            _peliculaOSerieRepository = peliculaOSerieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDto>>> GetGeneros()
        {
            return Ok(_mapper.Map<IEnumerable<GeneroDto>>(await _generoRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroDetailDto>> GetGenero(int id)
        {
            var genero = _mapper.Map<GeneroDetailDto>(await _generoRepository.Get(id));
            if(genero == null)
            {
                return NotFound();
            }
            return Ok(genero);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, GeneroCreateDto genero)
        {
            var g = await _generoRepository.Get(id);
            if (g == null)
            {
                return NotFound();
            }

            await _generoRepository.Update(_mapper.Map(genero, g));

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GeneroDto>> PostGenero([FromQuery]int[] peliculaOSerieId,GeneroCreateDto genero)
        {
            var g = _mapper.Map<Genero>(genero);
            g.PeliculasOSeries = new List<PeliculaOSerie>();
            foreach(int id in peliculaOSerieId)
            {
                var poc = await _peliculaOSerieRepository.Get(id);
                if(poc == null)
                {
                    ModelState.AddModelError(nameof(g.PeliculasOSeries), $"peliculasOSeriesId {id} does not exist");
                    return BadRequest(ModelState);
                }
                g.PeliculasOSeries.Add(poc);
            }
            var gdto = _mapper.Map<GeneroDto>(await _generoRepository.Add(g));
            return CreatedAtAction("GetGenero", new { id = gdto.Id }, gdto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _generoRepository.Get(id);
            if(genero == null)
            {
                return NotFound();
            }
            await _generoRepository.Delete(genero);
            return NoContent();
        }
    }
}
