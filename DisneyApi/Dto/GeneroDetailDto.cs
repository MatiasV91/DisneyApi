﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Dto
{
    public class GeneroDetailDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }

        public List<PeliculaOSerieDto> PeliculasOSeries { get; set; }
    }
}
