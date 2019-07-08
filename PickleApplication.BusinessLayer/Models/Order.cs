using System.Collections.Generic;

namespace PickleApplication.BusinessLayer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public Customer Customer { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
        public OrderStatus Status { get; set; }
        public int OrderLineCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProviderNote { get; set; }
    }
}
