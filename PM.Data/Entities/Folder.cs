using PM.Data.Entities.Bases;
using System.Collections.Generic;

namespace PM.Data.Entities
{
    public class Folder : IdBase<int>
    {
        public string Name { get; set; }
        public int? ParentFolderId { get; set; }
        public Folder ParentFolder { get; set; }
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public ICollection<Folder> ChildFolders { get; set; } = new HashSet<Folder>();
        public ICollection<UploadedFile> Files { get; set; } = new HashSet<UploadedFile>();
    }
}
