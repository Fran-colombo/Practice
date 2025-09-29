using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Repositories;
using Presentation.DTOs.ToDoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
        }

        public async Task<ToDoForViewDto?> ReadToDoById(Guid id)
        {
            var toDo = await _toDoRepository.ReadByIdAsync(id);
            return toDo == null ? null : _mapper.Map<ToDoForViewDto>(toDo);
        }

        public async Task<IEnumerable<ToDoForViewDto>?> ReadAllToDos()
        {
            var toDos = await _toDoRepository.ReadAllAsync();
            return toDos == null ? null : _mapper.Map<IEnumerable<ToDoForViewDto>>(toDos);
        }

        public async Task<IEnumerable<ToDoForViewDto>?> ReadToDosByUserId(Guid userId)
        {
            var toDos = await _toDoRepository.ReadToDosByUserIdAsync(userId);
            return toDos == null ? null : _mapper.Map<IEnumerable<ToDoForViewDto>>(toDos);
        }
        public async Task<Guid> CreateToDo(ToDoForCreationDto toDoDto)
        {
            var toDo = _mapper.Map<ToDo>(toDoDto);
            toDo.Id = Guid.NewGuid();
            toDo.CreatedAt = DateTime.UtcNow;
            var newToDoId = await _toDoRepository.CreateAsync(toDo);
            return newToDoId;
        }

        public async Task<int?> UpdateToDo(Guid id, ToDoForUpdateDto toDoDto)
        {
            var existingToDo = await _toDoRepository.ReadByIdAsync(id);
            if (existingToDo == null)
            {
                return null;
            }
            _mapper.Map(toDoDto, existingToDo);
            existingToDo.UpdatedAt = DateTime.UtcNow;
            return await _toDoRepository.UpdateAsync(existingToDo);
        }

        public async Task<int?> DeleteToDo(Guid id)
        {
            return await _toDoRepository.DeleteAsync(id);
        }
    }
}
