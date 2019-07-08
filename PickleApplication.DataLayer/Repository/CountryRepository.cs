using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class CountryRepository : RepositoryBase, ICountryRepository
    {
        public async Task<bool> Create(Country entity)
        {
            var query = @"SELECT CASE WHEN EXISTS 
                        (INSERT INTO [dbo].[Country]
                               ([CountryName]
                               ,[CreatedOn]
                               ,[ModifiedOn])
                         VALUES
                               (@CountryName
                               ,@CreatedOn
                               ,@ModifiedOn))
                        THEN CAST(1 AS BIT)
                        ELSE CAST(0 AS BIT) END";
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
            var query = @"DELETE FROM [dbo].[Country]
                          WHERE Id = @Id";
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

        public async Task<IEnumerable<Country>> GetAll()
        {
            var query = @"SELECT [Id]
                            ,[CountryName]
                            ,[CreatedOn]
                            ,[ModifiedOn]
                        FROM [dbo].[Country]";
            return await _dbConnection.QueryAsync<Country>(query);
        }

        public async  Task<Country> GetById(int id)
        {
            var query = @"SELECT [Id]
                            ,[CountryName]
                            ,[CreatedOn]
                            ,[ModifiedOn]
                        FROM [dbo].[Country]
                        WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Country>(query, new { Id = id });
        }

        public async Task<bool> Update(Country entity)
        {
            var query = @"UPDATE [dbo].[Country]
                           SET [CountryName] = @CountryName
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
