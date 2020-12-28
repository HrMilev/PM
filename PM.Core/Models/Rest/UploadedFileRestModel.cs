using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class UploadedFileRestModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
