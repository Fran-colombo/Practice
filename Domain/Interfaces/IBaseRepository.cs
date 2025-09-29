using Domain.Interfaces;

namespace Infraestructure.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<Guid> CreateAsync(T entity);
        Task<IEnumerable<T?>?> ReadAllAsync();
        Task<T?> ReadByIdAsync(Guid id);
        Task<int?> UpdateAsync(T entity);
        Task<int?> DeleteAsync(Guid id);
    }
}