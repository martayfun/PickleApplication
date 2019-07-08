using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class LinkRepository : RepositoryBase, ILinkRepository
    {
        public async Task<bool> Create(Link entity)
        {
            var query = @"INSERT INTO [dbo].[Link]
                            ([ParentId]
                            ,[Type]
                            ,[Name]
                            ,[Description]
                            ,[IsActive]
                            ,[IsAssigned]
                            ,[CreatedOn]
                            ,[ModifiedOn])
                        VALUES
                            (@ParentId
                            ,@Type
                            ,@Name
                            ,@Description
                            ,@IsActive
                            ,@IsAssigned
                            ,@CreatedOn
                            ,@ModifiedOn)";
            try
            {
                await _dbConnection.ExecuteAsync(query, entity);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var query = @"DELETE FROM [dbo].[Link] WHERE Id = @Id";
            try
            {
                await _dbConnection.ExecuteAsync(query, new { Id = id });
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Link>> GetAll()
        {
            var query = @"SELECT [Id]
                            ,[ParentId]
                            ,[Type]
                            ,[Name]
                            ,[Description]
                            ,[IsActive]
                            ,[IsAssigned]
                            ,[CreatedOn]
                            ,[ModifiedOn]
                        FROM [dbo].[Link]";
            return await _dbConnection.QueryAsync<Link>(query);
        }

        public async Task<Link> GetById(int id)
        {
            var query = @"SELECT [Id]
                            ,[ParentId]
                            ,[Type]
                            ,[Name]
                            ,[Description]
                            ,[IsActive]
                            ,[IsAssigned]
                            ,[CreatedOn]
                            ,[ModifiedOn]
                        FROM [dbo].[Link]
                        WHERE Id = @Id";
            try
            {
                return (await _dbConnection.QueryAsync<Link>(query, new { Id = id })).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public async Task<IEnumerable<Link>> GetLinksByType(MainType type)
        {
            var query = @"SELECT [Id]
                            ,[ParentId]
                            ,[Type]
                            ,[Name]
                            ,[Description]
                            ,[IsActive]
                            ,[IsAssigned]
                            ,[CreatedOn]
                            ,[ModifiedOn]
                        FROM [dbo].[Link]
                        WHERE Type = @Type";
            try
            {
                return await _dbConnection.QueryAsync<Link>(query, new { Type = type });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task IsAssignedFalse(int id)
        {
            var query = @"UPDATE Link SET IsAssigned = 0 WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task IsAssignedTrue(int id)
        {
            var query = @"UPDATE Link SET IsAssigned = 1 WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<bool> Update(Link entity)
        {
            var query = @"UPDATE [dbo].[Link]
                        SET [ParentId] = @ParentId
                            ,[Type] = @Type
                            ,[Name] = @Name
                            ,[Description] = @Description
                            ,[IsActive] = @IsActive
                            ,[IsAssigned] = @IsAssigned
                            ,[CreatedOn] = @CreatedOn
                            ,[ModifiedOn] = @ModifiedOn
                        WHERE Id = @Id";
            try
            {
                await _dbConnection.ExecuteAsync(query, entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
