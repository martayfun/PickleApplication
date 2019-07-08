using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class OrderLineService : IOrderLineService
    {
        public OrderLineService(IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public async Task<bool> CreateOrderLine(OrderLine orderLine)
        {
            return await _orderLineRepository.Create(new DataLayer.Models.OrderLine
            {
                Id = orderLine.OrderLineId,
                Product = new DataLayer.Models.Product()
                {
                    Id = orderLine.Product.ProductId,
                    LinkId = orderLine.Product.Link.LinkId,
                    Description = orderLine.Product.Description,
                    ProductCode = orderLine.Product.ProductCode,
                    ProductName = orderLine.Product.ProductName,
                    Star = orderLine.Product.Star,
                    Stock = orderLine.Product.Stock,
                    Title = orderLine.Product.Title,
                    UnitPrice = orderLine.Product.UnitPrice
                }
            });
        }

        public async Task<bool> DeleteOrderLine(int id)
        {
            return await _orderLineRepository.Delete(id);
        }

        public async Task<OrderLine> GetOrderLineById(int id)
        {
            var orderLine = await _orderLineRepository.GetById(id);
            if (orderLine == null) return null;
            return new OrderLine
            {
                OrderLineId = orderLine.Id,
                Product = new Product
                {
                    ProductId = orderLine.Product.Id,
                    Link = new Link
                    {
                        LinkId = orderLine.Product.Link.Id,
                        Name = orderLine.Product.Link.Name
                    },
                    ProductName = orderLine.Product.ProductName,
                    ProductCode = orderLine.Product.ProductCode,
                    Star = orderLine.Product.Star,
                    Stock = orderLine.Product.Stock,
                    Title = orderLine.Product.Title,
                    UnitPrice = orderLine.Product.UnitPrice
                }
            };
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLines()
        {
            return (await _orderLineRepository.GetAll()).Select(o => new OrderLine
            {
                OrderLineId = o.Id,
                Product = new Product
                {
                    ProductId = o.Product.Id,
                    Link = new Link
                    {
                        LinkId = o.Product.Link.Id,
                        Name = o.Product.Link.Name
                    },
                    Description = o.Product.Description,
                    ProductCode = o.Product.ProductCode,
                    ProductName = o.Product.ProductName,
                    Star = o.Product.Star,
                    Stock = o.Product.Stock,
                    Title = o.Product.Title,
                    UnitPrice = o.Product.UnitPrice
                }
            });
        }

        public async Task<bool> UpdateOrderLine(OrderLine orderLine)
        {
            return await _orderLineRepository.Update(new DataLayer.Models.OrderLine
            {
                Id = orderLine.OrderLineId,
                Product = new DataLayer.Models.Product
                {
                    Id = orderLine.Product.ProductId,
                    LinkId = orderLine.Product.Link.LinkId,
                    Description = orderLine.Product.Description,
                    ProductCode = orderLine.Product.ProductCode,
                    ProductName = orderLine.Product.ProductName,
                    Star = orderLine.Product.Star,
                    Stock = orderLine.Product.Stock,
                    Title = orderLine.Product.Title,
                    UnitPrice = orderLine.Product.UnitPrice
                }
            });
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLineByOrderId(int orderId)
        {
            return (await _orderLineRepository.GetByOrderId(orderId)).Select(o => new OrderLine
            {
                OrderLineId = o.Id,
                Product = new Product
                {
                    ProductId= o.Product.Id,
                    Description = o.Product.Description,
                    ProductCode = o.Product.ProductCode,
                    ProductName = o.Product.ProductName,
                    Star = o.Product.Star,
                    Stock = o.Product.Stock,
                    Title = o.Product.Title,
                    UnitPrice = o.Product.UnitPrice,
                    Link = new Link
                    {
                        LinkId=o.Product.Link.Id,
                        Description = o.Product.Link.Description,
                        IsActive = o.Product.Link.IsActive,
                        IsAssigned = o.Product.Link.IsAssigned,
                        Name = o.Product.Link.Name,
                        ParentId = o.Product.Link.ParentId,
                        Type = (MainType)o.Product.Link.Type
                    }
                }
            });
        }

        private readonly IOrderLineRepository _orderLineRepository;
    }
}
