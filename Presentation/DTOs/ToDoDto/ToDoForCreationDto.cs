using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs.ToDoDto
{
    public class ToDoForCreationDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime createdAt { get; set; }
        
    }
}
