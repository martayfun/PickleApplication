using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class CityService : ICityService
    {
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            return (await _cityRepository.GetAll()).Select(p => new City
            {
                CityId = p.Id,
                CityName = p.CityName,
                Country = new Country
                {
                    CountryId = p.Country.Id,
                    CountryName = p.Country.CountryName
                }
            }).ToList();
        }

        public async Task<bool> CreateCity(City city)
        {
            return await _cityRepository.Create(new DataLayer.Models.City
            {
                CityName = city.CityName,
                CountryId = city.Country.CountryId
            });
        }

        public async Task<bool> UpdateCity(City city)
        {
            return await _cityRepository.Update(new DataLayer.Models.City
            {
                Id = city.CityId,
                CityName = city.CityName,
                CountryId = city.Country.CountryId
            });
        }

        public async Task<City> GetCityById(int id)
        {
            var city = await _cityRepository.GetById(id);
            return new City
            {
                CityId = city.Id,
                CityName = city.CityName,
                Country = new Country
                {
                    CountryId = city.Country.Id,
                    CountryName = city.Country.CountryName
                }
            };
        }

        public async Task<bool> DeleteCityById(int id)
        {
            return await _cityRepository.Delete(id);
        }

        private readonly ICityRepository _cityRepository;
    }
}
