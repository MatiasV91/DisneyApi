using DisneyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisneyApi.Data
{
    public interface IPersonajeRepository
    {
        Task<Personaje> Add(Personaje personaje);
        Task Delete(Personaje personaje);
        Task<Personaje> Get(int id);
        Task<IEnumerable<Personaje>> GetAll(int? age,int? idMovie, string name);
        Task Update(Personaje personaje);
    }
}