using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class City : Entity<int>
    {
        public City()
        {
            CreatedOn = DateTime.Now;
            CreatedOn = DateTime.Now;
        }
        public string CityName { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
