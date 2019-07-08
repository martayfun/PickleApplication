using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class Link : Entity<int>
    {
        public Link()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
        public MainType Type { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
