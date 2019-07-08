using System;
using System.Data;
using System.Data.SqlClient;

namespace PickleApplication.DataLayer.Repository
{
    public class RepositoryBase : IDisposable
    {
         public static SqlConnection _dbConnection;
 
        public RepositoryBase()
        {
            _dbConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
 
        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
