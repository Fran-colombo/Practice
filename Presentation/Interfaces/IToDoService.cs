using Presentation.DTOs.ToDoDto;

namespace Application.Interfaces
{
    public interface IToDoService
    {
        Task<Guid> CreateToDo(ToDoForCreationDto toDoDto);
        Task<int?> DeleteToDo(Guid id);
        Task<IEnumerable<ToDoForViewDto>?> ReadAllToDos();
        Task<ToDoForViewDto?> ReadToDoById(Guid id);
        Task<IEnumerable<ToDoForViewDto>?> ReadToDosByUserId(Guid userId);
        Task<int?> UpdateToDo(Guid id, ToDoForUpdateDto toDoDto);
    }
}