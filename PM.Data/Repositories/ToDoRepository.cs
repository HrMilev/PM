﻿using PM.Data;
using PM.Data.Entities.ToDos;
using PM.Data.Repositories.Bases;
using PM.Data.Repositories.Interfaces;

namespace PM.Data.Repositories
{
    public class ToDoRepository : RepositoryBase<ToDo>, IToDoRepository
    {
        public ToDoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}