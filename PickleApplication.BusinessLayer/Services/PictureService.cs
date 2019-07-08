using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public class PictureService : IPictureService
    {
        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }
        public async Task<bool> CreatePicture(Picture picture)
        {
            return await _pictureRepository.Create(new DataLayer.Models.Picture
            {
                Id = picture.PictureId,
                ProductId = picture.ProductId,
                Extension = picture.Extension,
                IsActive = picture.IsActive
            });
        }

        public async Task<bool> DeletePictureById(string id)
        {
            return await _pictureRepository.DeleteByPictureId(id);
        }

        public async Task<Picture> GetPictureById(string id)
        {
            DataLayer.Models.Picture picture = await _pictureRepository.GetByPictureId(id);
            return new Picture
            {
                PictureId = picture.Id,
                ProductId = picture.ProductId,
                Extension = picture.Extension,
                IsActive = picture.IsActive
            };
        }

        public async Task<IEnumerable<Picture>> GetPictures()
        {
            return (await _pictureRepository.GetAll()).Select(p => new Picture
            {
                PictureId = p.Id,
                ProductId = p.ProductId,
                Extension = p.Extension,
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<bool> UpdatePicture(Picture picture)
        {
            return await _pictureRepository.Update(new DataLayer.Models.Picture
            {
                Id = picture.PictureId,
                ProductId = picture.ProductId,
                Extension = picture.Extension,
                IsActive = picture.IsActive
            });
        }

        public async Task<IEnumerable<Picture>> GetPicturesByProductId(int id)
        {
            return (await _pictureRepository.GetPicturesByProductId(id)).Select(p => new Picture
            {
                PictureId = p.Id,
                ProductId = p.ProductId,
                Extension = p.Extension,
                IsActive = p.IsActive
            }).ToList();
        }

        private readonly IPictureRepository _pictureRepository;
    }
}
