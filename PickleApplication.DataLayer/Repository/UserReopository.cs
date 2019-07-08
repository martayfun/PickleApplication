using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Mapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class UserReopository : RepositoryBase, IUserReopository
    {
        public async Task<bool> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsernameAndPassword(User user)
        {
            string query = @"SELECT * FROM [User] WHERE [UserName] = @UserName AND [Password] = @Password";

            try
            {
                return (await _dbConnection.QueryAsync<User>(query, user)).Single();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public async Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
