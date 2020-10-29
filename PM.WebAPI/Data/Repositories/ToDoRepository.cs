using PM.WebAPI.Data.Repositories.Base;
using PM.WebAPI.Data.Repositories.Interfaces;
using PM.WebAPI.Models.Entities.ToDoEntities;

namespace PM.WebAPI.Data.Repositories
{
    public class ToDoRepository : RepositoryBase<ToDo>, IToDoRepository
    {
        public ToDoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
