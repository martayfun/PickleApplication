using PickleApplication.DataLayer.Core;

namespace PickleApplication.DataLayer.Models
{
    public class Product : Entity<int>
    {
        public Product()
        {
            CreatedOn = System.DateTime.Now;
            ModifiedOn = System.DateTime.Now;
        }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public virtual Link Link { get; set; }
        public Picture Picture { get; set; }
        public int LinkId { get; set; }
        public int Star { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
