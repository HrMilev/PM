using PM.Data.Entities;
using PM.Data.Repositories.Bases;
using PM.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Data.Repositories
{
    public class ContactUsFormRepository : RepositoryBase<ContactUsForm>, IContactUsFormRepository
    {
        public ContactUsFormRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
