using Microsoft.AspNetCore.Identity;
using PM.WebAPI.Models.Entities.ToDoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ToDo> ToDos { get; set; } = new HashSet<ToDo>();
    }
}
