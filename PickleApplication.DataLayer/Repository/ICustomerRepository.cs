using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<int> CreateAndReturnCustomerId(Customer entity);
    }
}
