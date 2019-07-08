using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class LinkService : ILinkService
    {
        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        private readonly ILinkRepository _linkRepository;

        public async Task<IEnumerable<Link>> GetLinks()
        {
            return (await _linkRepository.GetAll()).Select(l => new Link
            {
                LinkId = l.Id,
                Name = l.Name,
                ParentId = l.ParentId,
                Type = (MainType)l.Type,
                Description = l.Description,
                IsActive = l.IsActive,
                IsAssigned = l.IsAssigned
            }).ToList();
        }

        public async Task<Link> GetLinkById(int id)
        {
            var link = await _linkRepository.GetById(id);
            if (link == null) return null;
            return new Link
            {
                LinkId = link.Id,
                Name = link.Name,
                ParentId = link.ParentId,
                Description = link.Description,
                IsActive = link.IsActive,
                Type = (MainType)link.Type,
                IsAssigned = link.IsAssigned
            };
        }

        public async Task<bool> LinkUpdate(Link link)
        {
            return await _linkRepository.Update(new DataLayer.Models.Link
            {
                Id = link.LinkId,
                Name = link.Name,
                Description = link.Description,
                IsActive = link.IsActive,
                ParentId = link.ParentId == null ? 0 : link.ParentId,
                Type = (DataLayer.Models.MainType)link.Type
            });
        }

        public async Task<bool> LinkCreate(Link link)
        {
            return await _linkRepository.Create(new DataLayer.Models.Link
            {
                Id = link.LinkId,
                Name = link.Name,
                Description = link.Description,
                IsActive = link.IsActive,
                ParentId = link.ParentId == null ? 0 : link.ParentId,
                Type = (DataLayer.Models.MainType)link.Type
            });
        }

        public async Task<bool> LinkDeleteById(int id)
        {
            return await _linkRepository.Delete(id);
        }

        public async Task<IEnumerable<Link>> GetLinksByType(MainType type)
        {
            return (await _linkRepository.GetLinksByType((DataLayer.Models.MainType)type)).Select(l => new Link
            {
                LinkId = l.Id,
                Name = l.Name,
                ParentId = l.ParentId,
                Type = (MainType)l.Type,
                Description = l.Description,
                IsActive = l.IsActive,
                IsAssigned = l.IsAssigned

            }).ToList();
        }
    }
}
