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
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPeliculaOSerieRepository _peliculaOserieRepository;
        private readonly IPersonajeRepository _personajeRepository;

        public MoviesController(IMapper mapper, IPeliculaOSerieRepository peliculaOserieRepository, IPersonajeRepository personajeRepository)
        {
            _mapper = mapper;
            _peliculaOserieRepository = peliculaOserieRepository;
            _personajeRepository = personajeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculasOSeriesDto>>> GetMovies([FromQuery]string name,[FromQuery]string order,[FromQuery]int? idGenero )
        {
            return Ok(_mapper.Map<IEnumerable<PeliculasOSeriesDto>>(await _peliculaOserieRepository.GetAll(name,order,idGenero)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaOSerieDetailDto>> GetMovie(int id)
        {
            var movie = _mapper.Map<PeliculaOSerieDetailDto>(await _peliculaOserieRepository.Get(id));
            if(movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, PeliculaOSerieCreateDto movie)
        {
            var m = await _peliculaOserieRepository.Get(id);
            if(m == null)
            {
                return NotFound();
            }
            await _peliculaOserieRepository.Update(_mapper.Map(movie, m));
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PeliculaOSerieDetailDto>> PostMovie([FromQuery]int[] personajeId,[FromBody]PeliculaOSerieCreateDto movie)
        {
            var m = _mapper.Map<PeliculaOSerie>(movie);

            m.Personajes = new List<Personaje>();
            foreach (int id in personajeId)
            {
                var sOp = await _personajeRepository.Get(id);
                if (sOp == null)
                {
                    ModelState.AddModelError(nameof(m.Personajes), $"personajeId {id} does not exist");
                    return BadRequest(ModelState);
                }
                m.Personajes.Add(sOp);
            }

            var md = _mapper.Map<PeliculaOSerieDetailDto>(await _peliculaOserieRepository.Add(m));
            return CreatedAtAction("GetMovie", new { id = md.Id }, md);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var m = await _peliculaOserieRepository.Get(id);
            if(m == null)
            {
                return NotFound();
            }
            await _peliculaOserieRepository.Delete(m);
            return NoContent();
        }
    }
}
