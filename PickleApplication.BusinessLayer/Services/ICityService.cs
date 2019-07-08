using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities();
        Task<City> GetCityById(int id);
        Task<bool> UpdateCity(City city);
        Task<bool> CreateCity(City city);
        Task<bool> DeleteCityById(int id);
    }
}
