using System;
using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class ToDoRestModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        public bool IsFinished { get; set; }
    }
}
