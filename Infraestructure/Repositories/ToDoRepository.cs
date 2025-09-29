using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ToDoRepository : BaseRepository<ToDo> , IToDoRepository
    {
        private readonly PracticeContext _context;
        public ToDoRepository(PracticeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDo>?> ReadToDosByUserIdAsync(Guid userId)
        {
            return await _context.ToDos
                                 .Where(t => t.UserId == userId && t.IsActive == ActiveEnum.Active)
                                 .ToListAsync();
        }
    }

}
