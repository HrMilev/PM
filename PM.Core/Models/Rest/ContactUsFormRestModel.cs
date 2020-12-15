using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class ContactUsFormRestModel
    {
        [StringLength(5000, MinimumLength = 50)]
        public string CreatorMessage { get; set; }
        [StringLength(500, MinimumLength = 10)]
        public string Subject { get; set; }
    }
}
