using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class OrderLineRepository : RepositoryBase, IOrderLineRepository
    {
        public async Task<bool> Create(OrderLine entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderLine>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderLine> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderLine>> GetByOrderId(int id)
        {
            var query = @"SELECT
                            orderLine.Id,
                            orderLine.Quantity,
                            product.Id,
                            product.ProductName,
                            product.ProductCode,
                            product.Title,
                            product.Description,
                            product.Star,
                            product.Stock,
                            product.UnitPrice,
                            link.Id,
                            link.Name,
	                        link.ParentId,
	                        link.IsActive,
	                        link.Type,
	                        link.IsAssigned,
	                        link.Description
                        FROM OrderLine as orderLine 
                        INNER JOIN Product as product ON orderLine.ProductId = product.Id 
                        INNER JOIN Link as link ON product.LinkId = link.Id 
                        WHERE link.Type = 2 AND orderLine.OrderId = @Id";
            try
            {
                return await _dbConnection.QueryAsync<OrderLine, Product, Link>(query, new { Id = id }, splitOn: "Id");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLineByOrderId(int orderId)
        {
            string query = @"SELECT 
	                            orderLine.Id,
	                            orderLine.ProductId,
	                            orderLine.OrderId,
	                            product.Id,
	                            product.ProductName,
	                            product.ProductCode,
	                            product.LinkId,
	                            product.Title,
	                            product.Description,
	                            product.Star,
	                            product.UnitPrice,
	                            link.Id,
	                            link.ParentId,
	                            link.Type,
	                            link.Name,
	                            link.Description,
	                            link.IsActive,
	                            link.IsAssigned
                            FROM [OrderLine] as orderLine
                            INNER JOIN [Product] as product ON orderLine.OrderId = product.Id 
                            INNER JOIN Link as link ON product.LinkId = link.Id WHERE orderLine.OrderId = @OrderId";
            try
            {
                return await _dbConnection.QueryAsync<OrderLine, Product, Link>(query);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> Update(OrderLine entity)
        {
            throw new NotImplementedException();
        }
    }
}
