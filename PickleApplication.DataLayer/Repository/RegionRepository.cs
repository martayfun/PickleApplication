using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class RegionRepository : RepositoryBase, IRegionRepository
    {
        public async Task<bool> Create(Region entity)
        {
            var query = @"INSERT INTO [dbo].[Region]
                               ([RegionName]
                               ,[CityId]
                               ,[CreatedOn])
                        VALUES
                               (@RegionName
                               ,@CityId
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
            var query = @"DELETE FROM [dbo].[Region] WHERE Id = @Id";
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

        public async Task<IEnumerable<Region>> GetAll()
        {
            var query = @"SELECT
                        reg.Id,
                        reg.RegionName,
                        reg.CityId,
                        ct.Id,
                        ct.CityName,
                        ct.CountryId,
                        cn.Id,
                        cn.CountryName
                        FROM Region reg INNER JOIN City ct ON reg.CityId = ct.Id INNER JOIN Country cn ON ct.CountryId = cn.Id";
            return await _dbConnection.QueryAsync<Region, City, Country>(query);
        }

        public async Task<Region> GetById(int id)
        {
            var query = @"SELECT
                        reg.Id,
                        reg.RegionName,
                        reg.CityId,
                        ct.Id,
                        ct.CityName,
                        ct.CountryId,
                        cn.Id,
                        cn.CountryName
                        FROM Region reg INNER JOIN City ct ON reg.CityId = ct.Id INNER JOIN Country cn ON ct.CountryId = cn.Id WHERE reg.Id = @Id";
            var regions = await _dbConnection.QueryAsync<Region, City, Country>(query, new { Id = id });
            return regions.FirstOrDefault();
        }

        public async Task<bool> Update(Region entity)
        {
            var query = @"UPDATE [dbo].[Region]
                           SET [RegionName] = @RegionName
                              ,[CityId] = @CityId
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
