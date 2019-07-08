using PickleApplication.BusinessLayer.Models;
using System.Threading.Tasks;

namespace PickleApplication.BusinessLayer.Services
{
    public interface IUserService
    {
        Task<User> GetByUsernameAndPassword(User user);
    }
}
