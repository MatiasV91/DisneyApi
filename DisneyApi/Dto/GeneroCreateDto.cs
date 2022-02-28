using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Dto
{
    public class GeneroCreateDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Imagen { get; set; }

    }
}
