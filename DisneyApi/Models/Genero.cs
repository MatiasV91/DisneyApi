using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Models
{
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Imagen { get; set; }

        public ICollection<PeliculaOSerie> PeliculasOSeries { get; set; }
    }
}
