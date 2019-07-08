using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class CityRepository : RepositoryBase, ICityRepository
    {
        public async Task<bool> Create(City entity)
        {
            var query = @"INSERT INTO [dbo].[City]
                               ([CityName]
                               ,[CountryId]
                               ,[CreatedOn]
                               ,[ModifiedOn])
                         VALUES
                               (@CityName
                               ,@CountryId
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
            var query = @"DELETE FROM [dbo].[City] WHERE Id = @Id";
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

        public async Task<IEnumerable<City>> GetAll()
        {
            var query = @"SELECT
                            city.Id,
                            city.CityName,
                            country.Id,
                            country.CountryName
                        FROM City city INNER JOIN Country country ON city.CountryId = country.Id";
            return await _dbConnection.QueryAsync<City, Country>(query, splitOn: "Id");
        }

        public async Task<City> GetById(int id)
        {
            var query = @"SELECT
	                        city.Id,
	                        city.CityName,
	                        country.Id,
	                        country.CountryName
                        FROM City city INNER JOIN Country country ON city.CountryId = country.Id
                        WHERE city.Id = @Id";
            var city = await _dbConnection.QueryAsync<City, Country>(query, new { Id = id });
            return city.FirstOrDefault();
        }

        public async Task<bool> Update(City entity)
        {
            var query = @"UPDATE [dbo].[City]
                           SET [CityName] = @CityName
                              ,[CountryId] = @CountryId
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
