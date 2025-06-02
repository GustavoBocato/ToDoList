using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public class ClientToDoListRepository : IClientToDoListRepository
    {
        private readonly ToDoListDbContext _dbContext;

        public ClientToDoListRepository(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void create(ClientToDoList clientToDoList) 
        {
            _dbContext.clienttodolists.Add(clientToDoList);
            _dbContext.SaveChanges();
        }
    }
}
