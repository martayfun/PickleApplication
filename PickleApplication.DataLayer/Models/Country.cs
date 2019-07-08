using PickleApplication.DataLayer.Core;
using System;
using System.Collections.Generic;

namespace PickleApplication.DataLayer.Models
{
    public class Country : Entity<int>
    {
        public Country()
        {
            ModifiedOn = DateTime.Now;
            CreatedOn = DateTime.Now;
        }
        public string CountryName { get; set; }
    }
}
