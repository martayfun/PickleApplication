using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public Task<bool> Create(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateOfReturnOrderId(Order order)
        {
            string query = @"INSERT INTO [dbo].[Order]
                                   ([CustomerId]
                                   ,[DeliveryInfoId]
                                   ,[TotalPrice]
                                   ,[Status]
                                   ,[OrderCode]
                                   ,[OrderLineCount]
                                   ,[ProviderNote]
                                   ,[CreatedOn])
                             VALUES
                                   (@CustomerId
                                   ,@DeliveryInfoId
                                   ,@TotalPrice
                                   ,@Status
                                   ,@OrderCode
                                   ,@OrderLineCount
                                   ,@ProviderNote
                                   ,@CreatedOn);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                return (await _dbConnection.QueryAsync<int>(query, order)).Single();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var query = @"SELECT
                            ord.Id,
                            ord.OrderCode,
                            ord.TotalPrice,
                            ord.Status,
                            ord.ProviderNote,
                            ord.OrderLineCount,
                            customer.Id,
                            customer.CustomerName,
                            customer.CustomerSurname,
                            customer.PostalCode,
                            customer.MobilePhone,
                            customer.CompanyPhone,
                            customer.Mail,
                            deliveryInfo.Id,
                            deliveryInfo.Address,
                            region.Id,
                            region.RegionName,
                            region.CityId,
                            city.Id,
                            city.CityName,
                            city.CountryId,
                            country.Id,
                            country.CountryName
                        FROM [Order] as ord 
                        INNER JOIN Customer as customer ON ord.CustomerId = customer.Id 
                        INNER JOIN DeliveryInfo as deliveryInfo ON ord.DeliveryInfoId = deliveryInfo.Id
                        INNER JOIN Region as region ON deliveryInfo.RegionId = region.Id
                        INNER JOIN City as city ON deliveryInfo.CityId = city.Id
                        INNER JOIN Country as country ON deliveryInfo.CountryId = country.Id";
            return await _dbConnection.QueryAsync<Order, Customer, DeliveryInfo, Region, City, Country>(query, splitOn: "Id");
        }

        public async Task<Order> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderById(int id)
        {
            string query = @"SELECT
                                [order].Id,
                                [order].OrderCode,
                                [order].TotalPrice,
                                [order].Status,
                                [order].ProviderNote,
                                [order].OrderLineCount,
                                customer.Id,
                                customer.CustomerName,
                                customer.CustomerSurname,
                                customer.PostalCode,
                                customer.MobilePhone,
                                customer.CompanyPhone,
                                customer.Mail,
                                deliveryInfo.Id,
                                deliveryInfo.Address,
                                region.Id,
                                region.RegionName,
                                region.CityId,
                                city.Id,
                                city.CityName,
                                city.CountryId,
                                country.Id,
                                country.CountryName
                            FROM [Order] as [order]
                            INNER JOIN Customer as customer ON [order].CustomerId = customer.Id 
                            INNER JOIN DeliveryInfo as deliveryInfo ON [order].DeliveryInfoId = deliveryInfo.Id
                            INNER JOIN Region as region ON deliveryInfo.RegionId = region.Id
                            INNER JOIN City as city ON deliveryInfo.CityId = city.Id
                            INNER JOIN Country as country ON deliveryInfo.CountryId = country.Id WHERE [order].Id = @Id";
            try
            {
                return (await _dbConnection.QueryAsync<Order, Customer, DeliveryInfo, Region, City, Country>(query, new { Id = id })).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Order();
            }
        }

        public async Task<Order> GetOrderByOrderCode(string orderCode)
        {
            string query = @"SELECT
	                            [order].Id,
	                            [order].OrderCode,
	                            [order].TotalPrice,
	                            [order].Status,
	                            [order].ProviderNote,
	                            [order].OrderLineCount,
	                            customer.Id,
	                            customer.CustomerName,
	                            customer.CustomerSurname,
	                            customer.PostalCode,
	                            customer.MobilePhone,
	                            customer.CompanyPhone,
	                            customer.Mail,
	                            deliveryInfo.Id,
	                            deliveryInfo.Address,
	                            region.Id,
	                            region.RegionName,
	                            region.CityId,
	                            city.Id,
	                            city.CityName,
	                            city.CountryId,
	                            country.Id,
	                            country.CountryName
                            FROM [Order] AS [order] 
                            INNER JOIN Customer AS customer ON [order].CustomerId = customer.Id 
                            INNER JOIN DeliveryInfo AS deliveryInfo ON [order].DeliveryInfoId = deliveryInfo.Id
                            INNER JOIN Region AS region ON deliveryInfo.RegionId = region.Id
                            INNER JOIN City AS city ON deliveryInfo.CityId = city.Id
                            INNER JOIN Country AS country ON deliveryInfo.CountryId = country.Id WHERE [order].OrderCode = @OrderCode";
            try
            {
                return (await _dbConnection.QueryAsync<Order, Customer, DeliveryInfo, Region, City, Country>(query, new { OrderCode = orderCode })).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Order();
            }
        }

        public async Task<bool> OrderApproval(int id)
        {
            var query = @"UPDATE [Order] SET Status = @Status WHERE Id = @Id";
            try
            {
                await _dbConnection.ExecuteAsync(query, new { Id = id, Status = OrderStatus.Approval });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> OrderCancel(int id)
        {
            var query = @"UPDATE [Order] SET Status = @Status WHERE Id = @Id";
            try
            {
                await _dbConnection.ExecuteAsync(query, new { Id = id, Status = OrderStatus.Cancel });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
