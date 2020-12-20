using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PM.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ToDo> ToDos { get; set; } = new HashSet<ToDo>();
        public ICollection<UserQuestion> UserQuestions { get; set; }
        public ICollection<UserQuestion> ContactUsResponses { get; set; }
    }
}
