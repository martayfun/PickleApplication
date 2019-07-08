using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class OrderLine : Entity<int>
    {
        public OrderLine()
        {
            CreatedOn = DateTime.Now;
        }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
