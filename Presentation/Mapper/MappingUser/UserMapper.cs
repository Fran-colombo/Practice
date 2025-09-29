using Application.DTOs.UserDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.MappingUser
{
    public class UserMapper : Profile
    {
        public UserMapper() {
            CreateMap<User, UserForView>();
            CreateMap<UserForCreation, User>();
            CreateMap<UserForAuthenticate, User>();
        }
    }
}
