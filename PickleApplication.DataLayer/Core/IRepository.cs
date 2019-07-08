using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickleApplication.DataLayer.Core
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
