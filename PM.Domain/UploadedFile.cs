using PM.Domain.Base;
using System;

namespace PM.Domain
{
    public class UploadedFile : IdBase<Guid>
    {
        public int? FolderId { get; set; }
        public Folder Folder { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
