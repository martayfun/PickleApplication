using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        public OrderService(IOrderRepository orderRepository, IOrderLineService orderLineService,
            ICustomerService customerService, IDeliveryInfoService deliveryInfoService, ICountryService countryService,
            IPictureService pictureService, IProductService productService)
        {
            _orderRepository = orderRepository;
            _orderLineService = orderLineService;
            _customerService = customerService;
            _deliveryInfoService = deliveryInfoService;
            _countryService = countryService;
            _pictureService = pictureService;
            _productService = productService;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = (await _orderRepository.GetAll()).Select(o => new Order
            {
                OrderId = o.Id,
                Customer = new Customer
                {
                    CutomerId = o.Customer.Id,
                    CustomerName = o.Customer.CustomerName,
                    CustomerSurname = o.Customer.CustomerSurname,

                    MobilePhone = o.Customer.MobilePhone,
                    CompanyPhone = o.Customer.CompanyPhone,
                    PostalCode = o.Customer.PostalCode,
                    Mail = o.Customer.Mail
                },
                DeliveryInfo = new DeliveryInfo
                {
                    DeliveryInfoId = o.DeliveryInfo.Id,
                    Address = o.DeliveryInfo.Address,
                    Region = new Region
                    {
                        RegionId = o.DeliveryInfo.RegionId,
                        RegionName = o.DeliveryInfo.Region.RegionName,
                        City = new City
                        {
                            CityId = o.DeliveryInfo.Region.City.Id,
                            CityName = o.DeliveryInfo.Region.City.CityName,
                            Country = new Country
                            {
                                CountryId = o.DeliveryInfo.Region.City.Country.Id,
                                CountryName = o.DeliveryInfo.Region.City.Country.CountryName
                            }
                        }

                    }
                },
                ProviderNote = o.ProviderNote,
                Status = Map(o.Status),
                TotalPrice = o.TotalPrice,
                OrderLineCount = o.OrderLineCount
            });
            foreach (var order in orders)
            {
                order.OrderLines = await _orderLineService.GetOrderLineByOrderId(order.OrderId);
            }
            return orders;
        }

        private OrderStatus Map(DataLayer.Models.OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case DataLayer.Models.OrderStatus.None:
                    return OrderStatus.None;
                case DataLayer.Models.OrderStatus.Cancel:
                    return OrderStatus.Cancel;
                case DataLayer.Models.OrderStatus.Approval:
                    return OrderStatus.Approval;
                case DataLayer.Models.OrderStatus.Pending:
                    return OrderStatus.Pending;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<bool> OrderApproval(int id)
        {
            return await _orderRepository.OrderApproval(id);
        }

        public async Task<bool> OrderCancel(int id)
        {
            return await _orderRepository.OrderCancel(id);
        }

        public async Task<int> CreateOfReturnOrderId(Order order)
        {
            var customerId = await _customerService.CreateAndReturnCustomerId(order.Customer);
            var deliveryInfoId = await _deliveryInfoService.CreateDeliveryInfoAndReturnDeliveryInfoId(order.DeliveryInfo);
           
            return await _orderRepository.CreateOfReturnOrderId(new DataLayer.Models.Order
            {
                OrderCode = Guid.NewGuid().ToString(),
                DeliveryInfoId = deliveryInfoId,
                CustomerId = customerId,
                ProviderNote = string.Empty,
                Status = DataLayer.Models.OrderStatus.Pending,
                TotalPrice = GetCalculateTotalPrice(order.OrderLines),
                OrderLineCount = order.OrderLines.Count()
            });
        }

        private decimal GetCalculateTotalPrice(IEnumerable<OrderLine> orderLines)
        {
            decimal price = 0;
            foreach (var orderLine in orderLines)
            {
                price += orderLine.Product.UnitPrice * orderLine.Quantity;

            }
            return price;
        }

        public Task<bool> OrderCreate(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            var orderLines = await _orderLineService.GetOrderLineByOrderId(order.Id);
            foreach (var orderLine in orderLines)
            {
                orderLine.Product.Pictures = await _pictureService.GetPicturesByProductId(orderLine.Product.ProductId);
            }
            
            return new Order
            {
                OrderId = order.Id,
                OrderCode = order.OrderCode,
                Customer = new Customer
                {
                    CutomerId = order.Customer.Id,
                    CompanyPhone = order.Customer.CompanyPhone,
                    MobilePhone = order.Customer.MobilePhone,
                    CustomerName = order.Customer.CustomerName,
                    CustomerSurname = order.Customer.CustomerSurname,
                    Mail = order.Customer.Mail,
                    PostalCode = order.Customer.PostalCode
                },
                DeliveryInfo = new DeliveryInfo
                {
                    DeliveryInfoId = order.DeliveryInfoId,
                    Address = order.DeliveryInfo.Address,
                    Region = new Region
                    {
                        RegionId = order.DeliveryInfo.RegionId,
                        RegionName = order.DeliveryInfo.Region.RegionName,
                        City = new City
                        {
                            CityId = order.DeliveryInfo.Region.City.CountryId,
                            CityName = order.DeliveryInfo.Region.City.CityName
                        }
                    }
                },
                OrderLineCount = order.OrderLineCount,
                TotalPrice = order.TotalPrice,
                Status = (OrderStatus)order.Status,
                ProviderNote = order.ProviderNote,
                OrderLines = orderLines
            };
        }

        public async Task<Order> GetOrderByOrderCode(string orderCode)
        {
            var order = await _orderRepository.GetOrderByOrderCode(orderCode);
            return new Order
            {
                OrderId = order.Id,
                OrderCode = order.OrderCode,
                OrderLineCount = order.OrderLineCount,
                ProviderNote = order.ProviderNote,
                Status = (OrderStatus)order.Status,
                TotalPrice = order.TotalPrice
            };
        }

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineService _orderLineService;
        private readonly ICustomerService _customerService;
        private readonly IDeliveryInfoService _deliveryInfoService;
        private readonly ICountryService _countryService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
    }
}
