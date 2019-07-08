using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Picture> GetProductByPictures(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryId(int id);
        Task<IEnumerable<Product>> GetProductOrderByIdDescOrTake(int count);
    }
}
