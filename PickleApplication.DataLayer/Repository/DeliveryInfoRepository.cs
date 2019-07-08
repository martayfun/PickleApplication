using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class DeliveryInfoRepository : RepositoryBase, IDeliveryInfoRepository
    {
        public async Task<bool> Create(DeliveryInfo entity)
        {
            string query = @"INSERT INTO [dbo].[DeliveryInfo]
                                   ([CityId]
                                   ,[CountryId]
                                   ,[RegionId]
                                   ,[Address]
                                   ,[CreatedOn]
                                   ,[ModifiedOn])
                             VALUES
                                   (@CityId
                                   ,@CountryId
                                   ,@RegionId
                                   ,@Address
                                   ,@CreatedOn
                                   ,@ModifiedOn)";
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

        public async Task<int> CreateDeliveryInfoAndReturnDeliveryInfoId(DeliveryInfo deliveryInfo)
        {
            string query = @"INSERT INTO [dbo].[DeliveryInfo]
                                   ([CityId]
                                   ,[CountryId]
                                   ,[RegionId]
                                   ,[Address]
                                   ,[CreatedOn]
                                   ,[ModifiedOn])
                             VALUES
                                   (@CityId
                                   ,@CountryId
                                   ,@RegionId
                                   ,@Address
                                   ,@CreatedOn
                                   ,@ModifiedOn);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                return (await _dbConnection.QueryAsync<int>(query, deliveryInfo)).Single();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryInfo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryInfo> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DeliveryInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
