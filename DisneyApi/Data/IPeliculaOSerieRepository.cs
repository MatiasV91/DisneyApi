using DisneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public interface IPeliculaOSerieRepository
    {
        Task<PeliculaOSerie> Add(PeliculaOSerie peliculaOSerie);
        Task Delete(PeliculaOSerie peliculaOSerie);
        Task<PeliculaOSerie> Get(int id);
        Task<IEnumerable<PeliculaOSerie>> GetAll(string name, string order, int? IdGenero);
        Task Update(PeliculaOSerie peliculaOSerie);
    }
}
