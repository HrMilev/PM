using PM.Data.Repositories.Bases;
using PM.Data.Repositories.Interfaces;
using PM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Data.Repositories
{
    public class UserQuestionRepository : RepositoryBase<UserQuestion>, IUserQuestionRepository
    {
        public UserQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
