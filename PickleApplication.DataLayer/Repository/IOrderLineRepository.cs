using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        Task<IEnumerable<OrderLine>> GetByOrderId(int id);
        Task<IEnumerable<OrderLine>> GetOrderLineByOrderId(int orderId);
    }
}
