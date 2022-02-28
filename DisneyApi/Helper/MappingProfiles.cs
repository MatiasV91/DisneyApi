using AutoMapper;
using DisneyApi.Dto;
using DisneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PeliculaOSerie, PeliculaOSerieDto>();
            CreateMap<PeliculaOSerie, PeliculasOSeriesDto>();
            CreateMap<PeliculaOSerieCreateDto, PeliculaOSerie>();
            CreateMap<PeliculaOSerie, PeliculaOSerieDetailDto>();
            CreateMap<Personaje, PersonajeDto>().ReverseMap();
            CreateMap<Personaje, PersonajeDetailDto>();
            CreateMap<Personaje, PersonajesDto>();
            CreateMap<PersonajeCreateDto, Personaje>();
            CreateMap<Genero, GeneroDto>().ReverseMap();
            CreateMap<GeneroCreateDto, Genero>().ReverseMap();
            CreateMap<Genero, GeneroDetailDto > ();

        }
    }
}
