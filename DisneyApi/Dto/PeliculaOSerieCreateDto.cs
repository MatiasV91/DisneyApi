using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Dto
{
    public class PeliculaOSerieCreateDto
    {
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaDeCreacion { get; set; }
        [Required]
        [Range(1, 5)]
        public int Calificacion { get; set; }
    }
}
