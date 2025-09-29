using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs.ToDoDto
{
    public class ToDoForViewDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public StatusEnum Status { get; set; } 
    }
}
