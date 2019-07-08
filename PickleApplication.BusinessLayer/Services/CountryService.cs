using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class CountryService : ICountryService
    {
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return (await _countryRepository.GetAll()).Select(c => new Country
            {
                CountryId = c.Id,
                CountryName = c.CountryName
            });
        }

        public async Task<Country> GetCountryById(int id)
        {
            var country = await _countryRepository.GetById(id);
            if (country == null)
                return null;
            return new Country
            {
                CountryId = country.Id,
                CountryName = country.CountryName
            };

        }

        public Task<bool> UpdateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCountry(Country country)
        {
            throw new NotImplementedException();
        }

        private readonly ICountryRepository _countryRepository;
    }
}
