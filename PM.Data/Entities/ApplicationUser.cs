using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PM.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ToDo> ToDos { get; set; } = new HashSet<ToDo>();
        public ICollection<UserQuestion> UserQuestions { get; set; } = new HashSet<UserQuestion>();
        public ICollection<UserQuestion> UserQuestionResponses { get; set; } = new HashSet<UserQuestion>();
        public ICollection<Folder> Folders { get; set; } = new HashSet<Folder>();
        public int? RootFolderId { get; set; }
        public Folder RootFolder { get; set; }
    }
}
