using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class PictureRepository : RepositoryBase, IPictureRepository
    {
        public async Task<bool> Create(Picture entity)
        {
            var query = @"INSERT INTO [dbo].[Picture]
                               ([Id]
                               ,[ProductId]
                               ,[Extension]
                               ,[IsActive]
                               ,[CreatedOn])
                         VALUES
                               (@Id
                               ,@ProductId
                               ,@Extension
                               ,@IsActive
                               ,@CreatedOn)";

            await _dbConnection.ExecuteAsync(query, entity);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var query = @"DELETE FROM [dbo].[Picture] WHERE Id = @Id";
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

        public async Task<bool> DeleteByPictureId(string id)
        {
            var query = @"DELETE FROM [dbo].[Picture] WHERE Id = @Id";
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

        public async Task<IEnumerable<Picture>> GetAll()
        {
            var query = @"SELECT [Id]
                            ,[ProductId]
                            ,[Extension]
                            ,[IsActive]
                            ,[CreatedOn]
                            ,[ModifiedOn]
                        FROM [dbo].[Picture]";
            return await _dbConnection.QueryAsync<Picture>(query);
        }

        public async Task<Picture> GetById(int id)
        {
            var query = @"SELECT 
                             [Id]
                            ,[ProductId]
                            ,[Extension]
                            ,[IsActive]
                        FROM [dbo].[Picture] WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Picture>(query, new { Id = id });
        }

        public async Task<Picture> GetByPictureId(string id)
        {
            var query = @"SELECT 
                             [Id]
                            ,[ProductId]
                            ,[Extension]
                            ,[IsActive]
                        FROM [dbo].[Picture] WHERE Id = @Id";
            return (await _dbConnection.QueryAsync<Picture>(query, new { Id = id })).FirstOrDefault();
        }

        public async Task<IEnumerable<Picture>> GetPicturesByProductId(int id)
        {
            var query = @"SELECT 
                             [Id]
                            ,[ProductId]
                            ,[Extension]
                            ,[IsActive]
                        FROM [dbo].[Picture] WHERE ProductId = @ProductId";

            return await _dbConnection.QueryAsync<Picture>(query, new { ProductId = id });
        }

        public async Task<bool> Update(Picture entity)
        {
            var query = @"UPDATE [dbo].[Picture]
                        SET  [Id] = @Id
                            ,[ProductId] = @ProductId
                            ,[Extension] = @Extension
                            ,[IsActive] = @IsActive
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
