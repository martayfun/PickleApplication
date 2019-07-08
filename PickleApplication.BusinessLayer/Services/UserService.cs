using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        public UserService(IUserReopository userReopository)
        {
            _userReopository = userReopository;
        }
        public async Task<User> GetByUsernameAndPassword(User user)
        {
            DataLayer.Models.User usr = await _userReopository.GetByUsernameAndPassword(new DataLayer.Models.User {
                Id = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Mail = user.Mail,
                Password = user.Password
            });
            return new User
            {
                UserId = usr.Id,
                Name = usr.Name,
                UserName = usr.UserName,
                Surname = usr.Surname,
                Mail = usr.Mail,
                Password = usr.Password
            };
        }

        private readonly IUserReopository _userReopository;
    }
}
