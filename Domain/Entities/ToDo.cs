using Domain.Enum;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ToDo : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
        public ActiveEnum IsActive { get; set; } = ActiveEnum.Active;
        public User User { get; set; }
    }
}
