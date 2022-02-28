using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Dto
{
    public class PeliculaOSerieDetailDto
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public int Calificacion { get; set; }

        public IEnumerable<PersonajeDto> Personajes { get; set; }
    }
}
