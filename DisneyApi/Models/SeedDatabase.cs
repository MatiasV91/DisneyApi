using DisneyApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Models
{
    public static class SeedDatabase
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Personajes.Any())
            {

                context.Personajes.AddRange(
                    new Personaje
                    {
                        Nombre = "Bambi",
                        Imagen = "http://example.com/image.jpg",
                        Edad = 5,
                        Historia = "Bambi ― as with most of his friends ― is similar to any deer in any forest. In his early youth, Bambi has the full measure of charm that many young mammals display, with his wide eyes, spindly legs, curious nature, and a good cute voice. As he grows he gradually becomes more mature, but even in young adulthood he always seems a very young buck, with a graceful build and a fairly naïve, shy nature. ",
                        Peso = 20,
                        PeliculasOSeries = new PeliculaOSerie[] 
                        { 
                            new PeliculaOSerie { Imagen = "http://example.com/image.jpg", Titulo = "Bambi", Calificacion = 3, FechaDeCreacion = new DateTime(1942,8,21) },
                            new PeliculaOSerie { Imagen = "http://example.com/image.jpg", Titulo = "Bambi 2", Calificacion = 2, FechaDeCreacion = new DateTime(2006,2,7) },
                        }
                    },
                    new Personaje
                    {
                        Nombre = "Iron Man",
                        Imagen = "http://example.com/image.jpg",
                        Edad = 48,
                        Historia = "A wealthy American business magnate, playboy, philanthropist, inventor and ingenious scientist, Anthony Edward Stark suffers a severe chest injury during a kidnapping. When his captors attempt to force him to build a weapon of mass destruction, he instead creates a mechanized suit of armor to save his life and escape captivity. Later, Stark develops his suit, adding weapons and other technological devices he designed through his company, Stark Industries. He uses the suit and successive versions to protect the world as Iron Man. Although at first concealing his true identity, Stark eventually publicly reveals himself to be Iron Man.",
                        Peso = 72,
                        PeliculasOSeries = new PeliculaOSerie[]
                        {
                            new PeliculaOSerie { Imagen = "http://example.com/image.jpg", Titulo = "Iron Man", Calificacion = 4, FechaDeCreacion = new DateTime(2008,4,14) },
                            new PeliculaOSerie { Imagen = "http://example.com/image.jpg", Titulo = "Iron Man 2", Calificacion = 4, FechaDeCreacion = new DateTime(2010,4,29) },
                        }
                    },
                    new Personaje
                    {
                        Nombre = "Mulan",
                        Imagen = "http://example.com/image.jpg",
                        Edad = 16,
                        Historia = "She is the strong-willed and tenacious daughter of a war veteran, who strives to uphold her family's honor. When her father is called back into battle to defend China from Shan Yu, Mulan opts to protect him by taking his place under the guise of a male soldier named Ping.",
                        Peso = 43,
                        PeliculasOSeries = new PeliculaOSerie[] 
                        { 
                            new PeliculaOSerie { Imagen = "http://example.com/image.jpg", Titulo = "Mulan", Calificacion = 4, FechaDeCreacion = new DateTime(1998,6,5) },
                            new PeliculaOSerie { Imagen = "http://example.com/image.jpg", Titulo = "Mulan", Calificacion = 4, FechaDeCreacion = new DateTime(2020,3,9) },
                        }
                    }
                    );
                context.SaveChanges();

                context.Generos.AddRange(
                    new Genero { Imagen = "http://example.com/image.jpg", Nombre = "Children's film", PeliculasOSeries = context.PeliculasOSeries.Take(2).ToList() },
                    new Genero { Imagen = "http://example.com/image.jpg", Nombre = "SuperHero", PeliculasOSeries = context.PeliculasOSeries.Skip(2).Take(2).ToList() },
                    new Genero { Imagen = "http://example.com/image.jpg", Nombre = "Adventure", PeliculasOSeries = context.PeliculasOSeries.Skip(4).Take(1).ToList() },
                    new Genero { Imagen = "http://example.com/image.jpg", Nombre = "Drama", PeliculasOSeries = context.PeliculasOSeries.Skip(5).Take(1).ToList() }
                    );

                context.SaveChanges();
            };
            
        }
    }
}
