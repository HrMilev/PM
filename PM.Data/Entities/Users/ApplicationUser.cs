using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using PM.Data.Entities.ToDos;

namespace PM.Data.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ToDo> ToDos { get; set; } = new HashSet<ToDo>();
    }
}
