using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IOrderLineService
    {
        Task<OrderLine> GetOrderLineById(int id);
        Task<IEnumerable<OrderLine>> GetOrderLines();
        Task<bool> UpdateOrderLine(OrderLine orderLine);
        Task<bool> CreateOrderLine(OrderLine orderLine);
        Task<bool> DeleteOrderLine(int id);
        Task<IEnumerable<OrderLine>> GetOrderLineByOrderId(int orderId);
    }
}
