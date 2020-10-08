using System;
using System.ComponentModel.DataAnnotations;

namespace PM.Core.Models.View
{
    public class ToDoViewModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
    }
}
