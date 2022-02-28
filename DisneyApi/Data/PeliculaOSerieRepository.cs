using DisneyApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public class PeliculaOSerieRepository : IPeliculaOSerieRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IGeneroRepository _generoRepository;

        public PeliculaOSerieRepository(ApplicationDbContext context, IGeneroRepository generoRepository)
        {
            _context = context;
            _generoRepository = generoRepository;
        }

        public async Task<PeliculaOSerie> Add(PeliculaOSerie peliculaOSerie)
        {
            var p = await _context.PeliculasOSeries.AddAsync(peliculaOSerie);
            await _context.SaveChangesAsync();
            return p.Entity;
        }

        public async Task Delete(PeliculaOSerie peliculaOSerie)
        {
            _context.PeliculasOSeries.Remove(peliculaOSerie);
            await _context.SaveChangesAsync();
        }

        public async Task<PeliculaOSerie> Get(int id)
        {
            return await _context.PeliculasOSeries.Include(p => p.Personajes).SingleOrDefaultAsync(p => p.Id == id);

        }

        public async Task<IEnumerable<PeliculaOSerie>> GetAll(string name = null, string order = null, int? IdGenero = null)
        {
            var poc = _context.PeliculasOSeries.Include(p => p.Personajes).AsQueryable();
            
            if (IdGenero != null)
            {
                var genero = await _generoRepository.Get(IdGenero.Value);
                poc = poc.Where(p => genero.PeliculasOSeries.Contains(p));
            }

            if (!String.IsNullOrEmpty(name))
                poc = poc.Where(p => p.Titulo.ToLower().Contains(name.ToLower()));
            
            if (order == "DESC")
                poc = poc.OrderByDescending(p => p.Titulo);
            else
                poc = poc.OrderBy(p => p.Titulo);

            return await poc.ToArrayAsync();
        }

        public async Task Update(PeliculaOSerie peliculaOSerie)
        {
            _context.PeliculasOSeries.Update(peliculaOSerie);
            await _context.SaveChangesAsync();
        }

    }
}
