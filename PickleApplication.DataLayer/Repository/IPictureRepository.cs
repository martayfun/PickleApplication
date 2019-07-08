using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Task<IEnumerable<Picture>> GetPicturesByProductId(int id);
        Task<Picture> GetByPictureId(string id);
        Task<bool> DeleteByPictureId(string id);
    }
}
