using DisneyApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public class PersonajeRepository : IPersonajeRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonajeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Personaje>> GetAll(int? age = null, int? idMovie = null, string name = null)
        {
            var personajes = _context.Personajes.Include(ps => ps.PeliculasOSeries).AsQueryable();

            if (idMovie != null)
                personajes = personajes.Where(p => p.PeliculasOSeries.Contains(new PeliculaOSerie { Id = idMovie.Value }));

            if (age != null)
                personajes = personajes.Where(p => p.Edad == age);
            
            if (!String.IsNullOrEmpty(name))
                personajes = personajes.Where(p => p.Nombre.ToLower().Contains(name.ToLower()));
            
            return await personajes.OrderBy(p => p.Nombre).ToArrayAsync();
        }

        public async Task<Personaje> Get(int id)
        {
            return await _context.Personajes.Include(ps => ps.PeliculasOSeries).SingleOrDefaultAsync(p => p.Id == id);
        }


        public async Task<Personaje> Add(Personaje personaje)
        {
            var p = await _context.Personajes.AddAsync(personaje);
            await _context.SaveChangesAsync();
            return p.Entity;
        }

        public async Task Delete(Personaje personaje)
        {
            _context.Personajes.Remove(personaje);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Personaje personaje)
        {
            _context.Personajes.Update(personaje);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Personaje>> GetByAge(int age)
        {
            return await _context.Personajes.Where(p => p.Edad == age).ToArrayAsync();
        }
    }
}
