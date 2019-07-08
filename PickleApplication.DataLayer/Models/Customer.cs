using PickleApplication.DataLayer.Core;
using System;


namespace PickleApplication.DataLayer.Models
{
    public class Customer : Entity<int>
    {
        public Customer()
        {
            CreatedOn = DateTime.Now;
        }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string PostalCode { get; set; }
        public string MobilePhone { get; set; }
        public string CompanyPhone { get; set; }
        public string Mail { get; set; }

    }
}
