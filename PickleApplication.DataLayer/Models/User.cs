using PickleApplication.DataLayer.Core;
using System;

namespace PickleApplication.DataLayer.Models
{
    public class User : Entity<int>
    {
        public User()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
        }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
