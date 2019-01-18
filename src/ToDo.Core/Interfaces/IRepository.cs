using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Core.SharedKernel;

namespace ToDo.Core.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<(int NumberOfPages, List<T> Items)> PageAsync<T>(int page, int pageSize) where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;

    }
}
