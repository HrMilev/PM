using System;
using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class ToDoRestModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
    }
}
