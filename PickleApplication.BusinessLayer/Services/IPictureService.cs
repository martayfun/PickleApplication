using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IPictureService
    {
        Task<IEnumerable<Picture>> GetPictures();
        Task<Picture> GetPictureById(string id);
        Task<bool> CreatePicture(Picture picture);
        Task<bool> UpdatePicture(Picture picture);
        Task<bool> DeletePictureById(string id);
        Task<IEnumerable<Picture>> GetPicturesByProductId(int id);
    }
}
