using PM.Data.Entities.Bases;
using PM.Data.Entities.Users;
using System;

namespace PM.Data.Entities.ToDos
{
    public class ToDo : IdBase
    {
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsFinished { get; set; }
    }
}
