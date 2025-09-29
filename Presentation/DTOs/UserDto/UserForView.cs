using Presentation.DTOs.ToDoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDto
{
    public class UserForView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ToDoForViewDto> ToDos { get; set; }
    }
}
