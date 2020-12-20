using System;
using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class UserQuestionRestModel
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string CreatorMessage { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Subject { get; set; }
        public DateTime? CreateDate { get; set; }
        [StringLength(5000, MinimumLength = 50)]
        public string ResponderMessage { get; set; }
    }
}
