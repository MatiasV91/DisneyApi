using DisneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Dto
{
    public class PersonajeDetailDto
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Historia { get; set; }

        public IEnumerable<PeliculaOSerieDto> PeliculasOSeries{ get; set; }
}
}
