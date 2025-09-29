using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly PracticeContext _context;
        public BaseRepository(PracticeContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<T?>?> ReadAllAsync()
        {
            return await _context.Set<T>().Where(e => e.IsActive == ActiveEnum.Active).ToListAsync();
        }

        public async Task<T?> ReadByIdAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.IsActive == ActiveEnum.Active);
        }


        public async Task<int?> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.IsActive == ActiveEnum.Active);
            if (entity == null)
            {
                return 0; 
            }
            entity.IsActive = ActiveEnum.Inactive;
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
            
        }
    }
}
