using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs.ToDoDto;
using System.Security.Claims;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        private readonly IToDoService _toDoService;
        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDos()
        {
            var toDos = await _toDoService.ReadAllToDos();
            if (toDos == null || !toDos.Any())
            {
                return NotFound("No ToDos found.");
            }
            return Ok(toDos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoById(Guid id)
        {
            var toDo = await _toDoService.ReadToDoById(id);
            if (toDo == null)
            {
                return NotFound($"ToDo with ID {id} not found.");
            }
            return Ok(toDo);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetToDosByUserId(Guid userId)
        {
            var toDos = await _toDoService.ReadToDosByUserId(userId);
            if (toDos == null || !toDos.Any())
            {
                return NotFound($"No ToDos found for User ID {userId}.");
            }
            return Ok(toDos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDo([FromBody] ToDoForCreationDto toDoDto)
        {
            if (toDoDto == null)
            {
                return BadRequest("ToDo data is null.");
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("No se pudo identificar el usuario autenticado.");
            }
            toDoDto.UserId = Guid.Parse(userIdClaim.Value);
            var newToDoId = await _toDoService.CreateToDo(toDoDto);
            return CreatedAtAction(nameof(GetToDoById), new { id = newToDoId }, new { Id = newToDoId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDo(Guid id, [FromBody] ToDoForUpdateDto toDoDto)
        {
            if (toDoDto == null)
            {
                return BadRequest("ToDo data is null.");
            }
            var result = await _toDoService.UpdateToDo(id, toDoDto);
            if (result == null)
            {
                return NotFound($"ToDo with ID {id} not found.");
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(Guid id)
        {
            var result = await _toDoService.DeleteToDo(id);
            if (result == 0)
            {
                return NotFound($"ToDo with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
