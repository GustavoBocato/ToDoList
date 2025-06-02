using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListDbContext _dbContext;
        public ToDoListRepository(ToDoListDbContext toDoListDbContext) 
        {
            _dbContext = toDoListDbContext;
        }
        public void create(ToDoList toDoList)
        {
            _dbContext.todolists.Add(toDoList);
            _dbContext.SaveChanges();
        }

        public int retriveId(ToDoList toDoList) 
        {
            _dbContext.todolists.AsQueryable().FirstOrDefault(tdl => )
        }
    }
}
