using Domain.Entities;

namespace Infraestructure.Repositories
{
    public interface IToDoRepository : IBaseRepository<ToDo>
    {
       Task<IEnumerable<ToDo>?> ReadToDosByUserIdAsync(Guid userId);
    }
}