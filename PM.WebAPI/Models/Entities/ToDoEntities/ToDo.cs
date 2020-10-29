using PM.WebAPI.Models;
using PM.WebAPI.Models.Entities.BaseEntities;
using System;

namespace PM.WebAPI.Models.Entities.ToDoEntities
{
    public class ToDo : IdBase
    {
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
