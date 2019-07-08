using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class RegionService : IRegionService
    {
        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<IEnumerable<Region>> GetRegions()
        {
            var regions = await _regionRepository.GetAll();
            return regions.Select(r => new Region
            {
                RegionId = r.Id,
                RegionName = r.RegionName,
                City = new City
                {
                    CityId = r.City.Id,
                    CityName = r.City.CityName,
                    Country = new Country
                    {
                        CountryId = r.City.Country.Id,
                        CountryName = r.City.Country.CountryName
                    }
                }
            }).ToList();
        }

        public async Task<Region> GetRegionById(int id)
        {
            var region = await _regionRepository.GetById(id);
            if (region == null) return null;
            return new Region
            {
                RegionId = region.Id,
                RegionName = region.RegionName,
                City = new City
                {
                    CityId = region.City.Id,
                    CityName = region.City.CityName,
                    Country = new Country
                    {
                        CountryId = region.City.Country.Id,
                        CountryName = region.City.Country.CountryName
                    }
                }
            };
        }

        public async Task<bool> CreateRegion(Region region)
        {
            return await _regionRepository.Create(new DataLayer.Models.Region
            {
                Id = region.RegionId,
                RegionName = region.RegionName,
                CityId = region.City.CityId
            });
        }

        public async Task<bool> UpdateRegion(Region region)
        {
            return await _regionRepository.Update(new DataLayer.Models.Region
            {
                Id = region.RegionId,
                RegionName = region.RegionName,
                CityId = region.City.CityId
            });
        }

        public async Task<bool> DeleteRegionById(int id)
        {
            return await _regionRepository.Delete(id);
        }

        private readonly IRegionRepository _regionRepository;
    }
}
