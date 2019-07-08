using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> CreateAndReturnCustomerId(Customer customer)
        {
            return await _customerRepository.CreateAndReturnCustomerId(new DataLayer.Models.Customer {
                Id = customer.CutomerId,
                CustomerName = customer.CustomerName,
                CompanyPhone =customer.CompanyPhone,
                CustomerSurname = customer.CustomerSurname,
                Mail = customer.Mail,
                MobilePhone = customer.MobilePhone,
                PostalCode = customer.PostalCode
            });
        }

        private readonly ICustomerRepository _customerRepository;
    }
}
