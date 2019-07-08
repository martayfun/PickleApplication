using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<bool> OrderApproval(int id);
        Task<bool> OrderCancel(int id);
        Task<bool> OrderCreate(Order order);
        Task<int> CreateOfReturnOrderId(Order order);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByOrderCode(string orderCode);
    }
}
