using AutoMapper;
using Domain.Entities;
using Presentation.DTOs.ToDoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper.MappingToDo
{
    public class ToDoMapper : Profile
    {
        public ToDoMapper()
        {
            CreateMap<ToDo, ToDoForViewDto>();
            CreateMap<ToDoForCreationDto, ToDo>();
            CreateMap<ToDoForUpdateDto, ToDo>();
        }
    }
}
