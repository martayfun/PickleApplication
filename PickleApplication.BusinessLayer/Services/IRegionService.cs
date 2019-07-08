using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetRegions();
        Task<Region> GetRegionById(int id);
        Task<bool> CreateRegion(Region region);
        Task<bool> UpdateRegion(Region region);
        Task<bool> DeleteRegionById(int id);
    }
}
