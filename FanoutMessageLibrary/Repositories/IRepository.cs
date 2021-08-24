using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageLibrary.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAsync();
        Task<bool> CreateAsync(T entity);
    }
}
