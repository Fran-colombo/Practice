using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        ActiveEnum IsActive { get; set; }
    }
}
