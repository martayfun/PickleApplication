using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class ContentService : IContentService
    {
        public ContentService(IContentRepository ContentRepository, ILinkRepository linkRepository)
        {
            _contentRepository = ContentRepository;
            _linkRepository = linkRepository;
        }
        public async Task<Content> GetContentById(int id)
        {
            var content = await _contentRepository.GetById(id);
            if (content == null) return null;
            return new Content
            {
                ContentId = content.Id,
                ContentTitle = content.ContentTitle,
                ContentUrl = content.ContentUrl,
                ContentEditorText = content.ContentEditorText,
                ContentIsActive = content.ContentIsActive,
                Link = new Link
                {
                    LinkId = content.Link.Id,
                    Description = content.Link.Description,
                    IsActive = content.Link.IsActive,
                    Name = content.Link.Name,
                    ParentId = content.Link.ParentId,
                    Type = (MainType)content.Link.Type
                }
            };
        }

        public async Task<IEnumerable<Content>> GetContents()
        {
            return (await _contentRepository.GetAll()).Select(c => new Content
            {
                ContentId = c.Id,
                ContentTitle = c.ContentTitle,
                ContentUrl = c.ContentUrl,
                ContentEditorText = c.ContentEditorText,
                ContentIsActive = c.ContentIsActive,
                Link = new Link
                {
                    LinkId = c.Link.Id,
                    Description = c.Link.Description,
                    IsActive = c.Link.IsActive,
                    Name = c.Link.Name,
                    ParentId = c.Link.ParentId,
                    Type = (MainType)c.Link.Type
                }
            }).ToList();
        }

        public async Task<bool> ContentCreate(Content Content)
        {
            await _linkRepository.IsAssignedTrue(Content.Link.LinkId);
            return await _contentRepository.Create(new DataLayer.Models.Content
            {
                ContentTitle = Content.ContentTitle,
                ContentUrl = Content.ContentUrl,
                ContentEditorText = Content.ContentEditorText,
                ContentIsActive = Content.ContentIsActive,
                LinkId = Content.Link.LinkId
            });
        }

        public async Task<bool> ContentDeleteById(int id)
        {
            var content = await _contentRepository.GetById(id);
            await _linkRepository.IsAssignedFalse(content.Link.Id);
            return await _contentRepository.Delete(id);
        }

        public async Task<bool> ContentUpdate(Content Content)
        {
            var oldLink = await _contentRepository.GetById(Content.ContentId);
            if (Content.Link.LinkId != oldLink.LinkId)
            {
                await _linkRepository.IsAssignedFalse(oldLink.LinkId);
                await _linkRepository.IsAssignedTrue(Content.Link.LinkId);
            }
            return await _contentRepository.Update(new DataLayer.Models.Content
            {
                Id = Content.ContentId,
                ContentTitle = Content.ContentTitle,
                ContentUrl = Content.ContentUrl,
                ContentEditorText = Content.ContentEditorText,
                ContentIsActive = Content.ContentIsActive,
                LinkId = Content.Link.LinkId
            });
        }

        public async Task<Content> GetContentByLinkId(int linkId)
        {
            var content = await _contentRepository.GetByLinkId(linkId);
            if (content == null) return null;
            return new Content
            {
                ContentId = content.Id,
                ContentTitle = content.ContentTitle,
                ContentEditorText = content.ContentEditorText,
                ContentIsActive = content.ContentIsActive,
                ContentUrl = content.ContentUrl,
                Link = new Link
                {
                    LinkId = content.Link.Id,
                    ParentId = content.Link.ParentId,
                    Name = content.Link.Name,
                    Type = (MainType)content.Link.Type,
                    Description = content.Link.Description,
                    IsActive = content.Link.IsActive,
                    IsAssigned = content.Link.IsAssigned
                }
            };
        }

        private readonly IContentRepository _contentRepository;
        private readonly ILinkRepository _linkRepository;
    }
}
