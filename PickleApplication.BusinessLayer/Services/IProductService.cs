using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategoryId(int id);
        Task<Product> GetProductById(int id);
        Task<bool> DeleteProductById(int id);
        Task<bool> UpdateProduct(Product product);
        Task<bool> CreateProduct(Product product);
        Task<IEnumerable<Product>> GetProductOrderByIdDescOrTake(int count);
    }
}

