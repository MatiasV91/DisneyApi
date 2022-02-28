using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Dto
{
    public class PersonajeCreateDto
    {
        [Required]
        public string Imagen { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public double Peso { get; set; }
        [Required]
        public string Historia { get; set; }
    }
}
