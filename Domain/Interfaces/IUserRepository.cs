using Domain.Entities;

namespace Infraestructure.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> ReadByEmail(string email);
        Task<User?> Authenticate(User credentials);
    }
}