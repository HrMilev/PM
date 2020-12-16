using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class ContactUsFormRestModel
    {
        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string CreatorMessage { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Subject { get; set; }
    }
}
