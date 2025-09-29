using Application.DTOs.UserDto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateUserAsync(UserForAuthenticate credentials);
        Task<Guid> CreateUserAsync(UserForCreation user);
        Task<int?> DeleteUserAsync(Guid id);
        Task<UserForView?> ReadUserByEmailAsync(string email);
        Task<UserForView?> ReadUserByIdAsync(Guid id);
        Task<IEnumerable<UserForView>?> ReadUsersAsync();
    }
}