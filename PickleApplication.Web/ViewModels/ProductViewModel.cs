using PickleApplication.BusinessLayer.Models;
using System.Collections.Generic;

namespace PickleApplication.Web.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public Dictionary<int, string> Categories { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
}