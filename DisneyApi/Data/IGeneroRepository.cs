using DisneyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public interface IGeneroRepository
    {
        Task<Genero> Add(Genero genero);
        Task Delete(Genero genero);
        Task<Genero> Get(int id);
        Task<IEnumerable<Genero>> GetAll();
        Task Update(Genero genero);
    }
}
