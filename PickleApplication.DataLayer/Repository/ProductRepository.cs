using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {

        public async Task<bool> Create(Product entity)
        {
            var query = @"INSERT INTO [dbo].[Product]
                               ([ProductName],
                                [ProductCode],
                                [LinkId],
                                [Title],
                                [Description],
                                [Star],
                                [Stock],
                                [UnitPrice],
                                [CreatedOn])
                         VALUES
                               (@ProductName,
                                @ProductCode,
                                @LinkId, 
                                @Title,
                                @Description,
                                @Star,
                                @Stock,
                                @UnitPrice,
                                @CreatedOn)";
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
            var query = @"DELETE FROM [dbo].[Product]
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

        public async Task<IEnumerable<Product>> GetAll()
        {
            var query = @"SELECT 
                            product.Id,
                            product.ProductName,
                            product.ProductCode,
                            product.Title,
                            product.Description,
                            product.Star,
                            product.Stock,
                            product.UnitPrice,
	                        product.CreatedOn,
	                        product.ModifiedOn,
                            product.LinkId,
                            link.[Id],
	                        link.[Name],
	                        link.[Type],
	                        link.[ParentId],
	                        link.[Description],
	                        link.[IsActive]
                        FROM Product As product 
                        INNER JOIN Link As link ON product.LinkId = link.Id";
            return await _dbConnection.QueryAsync<Product, Link>(query, splitOn: "Id");
        }

        public async Task<Product> GetById(int id)
        {
            var query = @"SELECT 
                            product.Id,
                            product.ProductName,
                            product.ProductCode,
                            product.Title,
                            product.Description,
                            product.Star,
                            product.Stock,
                            product.UnitPrice,
	                        product.CreatedOn,
	                        product.ModifiedOn,
                            product.LinkId,
                            link.[Id],
	                        link.[Name],
	                        link.[Type],
	                        link.[ParentId],
	                        link.[Description],
	                        link.[IsActive]
                        FROM Product As product 
                        INNER JOIN Link As link ON product.LinkId = link.Id WHERE product.Id = @Id";
            var product = await _dbConnection.QueryAsync<Product, Link>(query, new { Id = id }, splitOn: "Id");
            return product.FirstOrDefault();
        }

        public IEnumerable<Picture> GetProductByPictures(int id)
        {
            var query = @"";
            return _dbConnection.Query<Picture>(query);
        }

        public Task<IEnumerable<Product>> GetProductOrderByIdDescOrTake(int count)
        {
            string query = @"SELECT 
	                            product.Id,
	                            product.ProductName,
	                            product.ProductCode,
	                            product.LinkId,
	                            product.Title,
	                            product.Description,
	                            product.Star,
	                            product.Stock,
	                            product.UnitPrice,
	                            link.Id,
	                            link.ParentId,
	                            link.Type,
	                            link.Name,
	                            link.Description,
	                            link.IsActive,
	                            link.IsAssigned
                            FROM product AS product 
                            INNER JOIN Link AS link ON product.LinkId = link.Id ORDER BY product.Id DESC
                            OFFSET 0 ROWS
                            FETCH NEXT @Count ROWS ONLY";

            return _dbConnection.QueryAsync<Product, Link>(query, new { Count = count});
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            var query = @"SELECT 
                            product.Id,
                            product.ProductName,
                            product.ProductCode,
                            product.Title,
                            product.Description,
                            product.Star,
                            product.Stock,
                            product.UnitPrice,
	                        product.CreatedOn,
	                        product.ModifiedOn,
                            product.LinkId,
                            link.[Id],
	                        link.[Name],
	                        link.[Type],
	                        link.[ParentId],
	                        link.[Description],
	                        link.[IsActive]
                        FROM Product As product
                        INNER JOIN Link As link ON product.LinkId = link.Id WHERE product.LinkId = @Id";
            return await _dbConnection.QueryAsync<Product, Link>(query, new { Id = id }, splitOn: "Id");
        }

        public async Task<bool> Update(Product entity)
        {
            var query = @"UPDATE [dbo].[Product]
                        SET [ProductName] = @ProductName
                            ,[ProductCode] = @ProductCode
                            ,[LinkId] = @LinkId
                            ,[Title] = @Title
                            ,[Description] = @Description
                            ,[Star] = @Star
                            ,[Stock] = @Stock
                            ,[UnitPrice] = @UnitPrice
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
