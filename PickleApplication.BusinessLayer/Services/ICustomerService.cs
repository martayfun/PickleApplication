using PickleApplication.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface ICustomerService
    {
        Task<int> CreateAndReturnCustomerId(Customer customer);
    }
}
