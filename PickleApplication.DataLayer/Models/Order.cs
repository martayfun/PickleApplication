using PickleApplication.DataLayer.Core;
using System;
using System.Collections.Generic;

namespace PickleApplication.DataLayer.Models
{
    public class Order : Entity<int>
    {
        public Order()
        {
            CreatedOn = DateTime.Now;
        }
        public string OrderCode { get; set; }
        public Customer Customer { get; set; }
        public DeliveryInfo DeliveryInfo { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryInfoId { get; set; }
        public OrderStatus Status { get; set; }
        public int OrderLineCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProviderNote { get; set; }
    }
}

