using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PM.Common.Models.Rest
{
    public class FolderRestModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        public int? ParentFolderId { get; set; }
        public IList<FolderRestModel> ChildFolders { get; set; }
    }
}
