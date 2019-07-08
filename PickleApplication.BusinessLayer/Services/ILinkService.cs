using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface ILinkService
    {
        Task<IEnumerable<Link>> GetLinks();
        Task<Link> GetLinkById(int id);
        Task<bool> LinkUpdate(Link link);
        Task<bool> LinkCreate(Link link);
        Task<bool> LinkDeleteById(int id);
        Task<IEnumerable<Link>> GetLinksByType(MainType type);
    }
}
