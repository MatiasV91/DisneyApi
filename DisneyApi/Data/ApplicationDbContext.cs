using DisneyApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Personaje> Personajes { get; set; }
        public DbSet<PeliculaOSerie> PeliculasOSeries { get; set; }
        public DbSet<Genero> Generos { get; set; }
    }
}
