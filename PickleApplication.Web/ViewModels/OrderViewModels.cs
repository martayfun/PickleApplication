using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;

namespace PickleApplication.Web.ViewModels
{
    public class OrderViewModels
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}