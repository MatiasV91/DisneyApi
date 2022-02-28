using DisneyApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly ApplicationDbContext _context;

        public GeneroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genero> Add(Genero genero)
        {
            var p = await _context.Generos.AddAsync(genero);
            await _context.SaveChangesAsync();
            return p.Entity;
        }

        public async Task Delete(Genero genero)
        {
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
        }

        public async Task<Genero> Get(int id)
        {
            return await _context.Generos.Include(p => p.PeliculasOSeries).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Genero>> GetAll()
        {
            return await _context.Generos.Include(p => p.PeliculasOSeries).ToArrayAsync();
        }

        public async Task Update(Genero genero)
        {
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
        }
    }
}
