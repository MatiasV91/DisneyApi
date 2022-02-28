using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Models
{
    public class PeliculaOSerie
    {
        public int Id { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaDeCreacion { get; set; }
        [Required]
        [Range(1,5)]
        public int Calificacion { get; set; }

        public ICollection<Personaje> Personajes { get; set; }
    }
}
