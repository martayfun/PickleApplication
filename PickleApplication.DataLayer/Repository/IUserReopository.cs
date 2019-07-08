using PickleApplication.DataLayer.Core;
using PickleApplication.DataLayer.Models;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Repository
{
    public interface IUserReopository : IRepository<User>
    {
        Task<User> GetByUsernameAndPassword(User user);
    }
}
