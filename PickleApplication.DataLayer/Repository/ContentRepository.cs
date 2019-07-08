using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class ContentRepository : RepositoryBase, IContentRepository
    {
        public async Task<bool> Create(Content entity)
        {
            var query = @"INSERT INTO [dbo].[Content]
                               ([LinkId]
                               ,[ContentTitle]
                               ,[ContentUrl]
                               ,[ContentIsActive]
                               ,[ContentEditorText]
                               ,[CreatedOn])
                         VALUES
                               (@LinkId
                               ,@ContentTitle
                               ,@ContentUrl
                               ,@ContentIsActive
                               ,@ContentEditorText
                               ,@CreatedOn)";
            try
            {
                await _dbConnection.ExecuteAsync(query, entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var query = @"DELETE FROM [dbo].[Content] WHERE Id = @Id";
            try
            {
                await _dbConnection.ExecuteAsync(query, new { Id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Content>> GetAll()
        {
            var query = @"SELECT
	                         hc.Id,
	                         hc.LinkId,
	                         hc.ContentTitle,
	                         hc.ContentUrl,
	                         hc.ContentIsActive,
	                         hc.ContentEditorText,
	                         L.Id,
	                         L.ParentId,
	                         L.Type,
	                         L.Name,
	                         L.Description,
	                         L.IsActive
                        FROM Content hc INNER JOIN Link l ON hc.LinkId = l.Id WHERE l.Type = 1 ORDER BY hc.CreatedOn DESC";
            return await _dbConnection.QueryAsync<Content, Link>(query, splitOn: "Id");
        }

        public async Task<Content> GetById(int id)
        {
            var query = @"SELECT
	                         hc.Id,
	                         hc.LinkId,
	                         hc.ContentTitle,
	                         hc.ContentUrl,
	                         hc.ContentIsActive,
	                         hc.ContentEditorText,
	                         L.Id,
	                         L.ParentId,
	                         L.Type,
	                         L.Name,
	                         L.Description,
	                         L.IsActive
                        FROM Content hc INNER JOIN Link l ON hc.LinkId = l.Id WHERE l.Type = 1 AND hc.Id = @Id ORDER BY hc.CreatedOn DESC";
            var content = await _dbConnection.QueryAsync<Content, Link>(query, new { Id = id });
            return content.FirstOrDefault();
        }

        public async Task<Content> GetByLinkId(int linkId)
        {
            var query = @"SELECT 
	                        content.Id,
	                        content.LinkId,
	                        content.ContentTitle,
	                        content.ContentUrl,
	                        content.ContentIsActive,
	                        content.ContentEditorText,
	                        link.Id,
	                        link.ParentId,
	                        link.Type,
	                        link.Name,
	                        link.Description,
	                        link.IsActive,
	                        link.IsAssigned
                        FROM dbo.Link AS link 
                        INNER JOIN dbo.Content AS content ON content.LinkId = link.Id WHERE content.LinkId = @LinkId AND content.ContentIsActive = 1";
            return (await _dbConnection.QueryAsync<Content, Link>(query, new { LinkId = linkId })).FirstOrDefault();
        }

        public async Task<bool> Update(Content entity)
        {
            var query = @"UPDATE [dbo].[Content]
                        SET [ContentTitle] = @ContentTitle
                            ,[LinkId] = @LinkId
                            ,[ContentUrl] = @ContentUrl
                            ,[ContentIsActive] = @ContentIsActive
                            ,[ContentEditorText] = @ContentEditorText
                            ,[ModifiedOn] = @ModifiedOn
                        WHERE Id = @Id";
            try
            {
                await _dbConnection.ExecuteAsync(query, entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
