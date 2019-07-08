using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<bool> OrderApproval(int id);
        Task<bool> OrderCancel(int id);
        Task<int> CreateOfReturnOrderId(Order order);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByOrderCode(string orderCode);
    }
}
