using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PickleApplication.DataLayer.Models;

namespace PickleApplication.DataLayer.Repository
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public async Task<bool> Create(Customer entity)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateAndReturnCustomerId(Customer entity)
        {
            string query = @"INSERT INTO [dbo].[Customer]
                                   ([CustomerName]
                                   ,[CustomerSurname]
                                   ,[PostalCode]
                                   ,[MobilePhone]
                                   ,[CompanyPhone]
                                   ,[Mail]
                                   ,[CreatedOn]
                                   ,[ModifiedOn])
                             VALUES
                                   (@CustomerName
                                   ,@CustomerSurname
                                   ,@PostalCode
                                   ,@MobilePhone
                                   ,@CompanyPhone
                                   ,@Mail
                                   ,@CreatedOn
                                   ,@ModifiedOn);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
               return (await _dbConnection.QueryAsync<int>(query, entity)).Single();
               
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
