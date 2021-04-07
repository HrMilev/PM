using PM.Domain.Base;
using System;

namespace PM.Domain
{
    public class ToDo : IdBase<Guid>
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
