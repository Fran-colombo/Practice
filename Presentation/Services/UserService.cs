using Application.DTOs.UserDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateUserAsync(UserForCreation user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");
            var existingUser = await _userRepository.ReadByEmail(user.Email);
            if (existingUser != null) throw new ArgumentException("User with this email already exists.", nameof(user.Email));
            try
            {
                var userEntity = _mapper.Map<User>(user);
                userEntity.Id = Guid.NewGuid();
                return await _userRepository.CreateAsync(userEntity);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid user data provided.", nameof(user));
            }
        }


        public async Task<UserForView?> ReadUserByIdAsync(Guid id)
        {
            var user = await _userRepository.ReadByIdAsync(id);
            var userForView = _mapper.Map<UserForView>(user);
            return userForView;
        }
        public async Task<IEnumerable<UserForView>?> ReadUsersAsync()
        {
            var users = await _userRepository.ReadAllAsync();
            var usersForView = _mapper.Map<IEnumerable<UserForView>>(users);
            return usersForView;
        }




        public async Task<int?> DeleteUserAsync(Guid id)
        {
            //if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            var userToDelete = await _userRepository.ReadByIdAsync(id);
            if (userToDelete == null) throw new ArgumentNullException(nameof(userToDelete));
            try
            {
                return await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error deleting user.", nameof(id));
            }
        }
        public async Task<User?> AuthenticateUserAsync(UserForAuthenticate credentials)
        {
            var credentialToAuthenticate = _mapper.Map<User>(credentials);
            return await _userRepository.Authenticate(credentialToAuthenticate);
        }


        public async Task<UserForView?> ReadUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

            var user = await _userRepository.ReadByEmail(email);
            if (user == null) throw new ArgumentNullException();
            var userForView = _mapper.Map<UserForView?>(user);
            return userForView;
        }
    }


}

