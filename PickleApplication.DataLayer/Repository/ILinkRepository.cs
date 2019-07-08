using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface ILinkRepository : IRepository<Link>
    {
        Task IsAssignedTrue(int id);
        Task IsAssignedFalse(int id);
        Task<IEnumerable<Link>> GetLinksByType(MainType type);
    }
}
