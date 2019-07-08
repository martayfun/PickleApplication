using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IContentService
    {
        Task<IEnumerable<Content>> GetContents();
        Task<Content> GetContentById(int id);
        Task<bool> ContentCreate(Content Content);
        Task<bool> ContentUpdate(Content Content);
        Task<bool> ContentDeleteById(int id);
        Task<Content> GetContentByLinkId(int linkId);
    }
}
