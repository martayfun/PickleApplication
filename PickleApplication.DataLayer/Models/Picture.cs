using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class Picture : Entity<string>
    {
        public Picture()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
        public int ProductId { get; set; }
        public string Extension { get; set; }
        public bool IsActive { get; set; }
    }
}
