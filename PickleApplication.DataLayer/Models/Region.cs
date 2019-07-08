using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class Region : Entity<int>
    {
        public Region()
        {
            ModifiedOn = DateTime.Now;
            CreatedOn = DateTime.Now;
        }
        public string RegionName { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}
