using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IContentRepository : IRepository<Content>
    {
        Task<Content> GetByLinkId(int linkId);
    }
}
