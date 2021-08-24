using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageLibrary.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAsync();
        Task<T> CreateAsync(T dto);
    }
}
