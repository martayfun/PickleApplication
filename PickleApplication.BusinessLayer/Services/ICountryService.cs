using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountryById(int id);
        Task<bool> UpdateCountry(Country country);
        Task<bool> CreateCountry(Country country);
    }
}
