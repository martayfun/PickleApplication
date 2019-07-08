using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class DeliveryInfo : Entity<int>
    {
        public DeliveryInfo()
        {
            CreatedOn = DateTime.Now;
        }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public string Address { get; set; }
    }
}
