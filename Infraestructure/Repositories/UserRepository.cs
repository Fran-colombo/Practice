using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PracticeContext _context;
        public UserRepository(PracticeContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<User?> ReadByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email), "Username cannot be null or empty.");
            return await _context.Users.Where(c => c.IsActive == ActiveEnum.Active).FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> Authenticate(User credentials)
        {
            return await _context.Users.Where(c => c.IsActive == ActiveEnum.Active).FirstOrDefaultAsync(u => u.Email == credentials.Email && u.Password == credentials.Password);
        }
    }
}
