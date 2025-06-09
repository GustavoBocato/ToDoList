using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertClient(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public Client? GetClientByEmail(string email)
        {
            return _dbContext.Clients.AsQueryable().FirstOrDefault(c => c.Email == email);
        }

        public Client? GetClientById(Guid clientId)
        {
            return _dbContext.Clients.AsQueryable().FirstOrDefault(c => c.Id == clientId);
        }

        public ToDoList CreateToDoList(ToDoList toDoList, Guid clientId)
        {
            var clientToDoList = new ClientToDoList()
            {
                IdClient = clientId,
                IdToDoList = toDoList.Id
            };

            _dbContext.ToDoLists.Add(toDoList);
            _dbContext.ClientToDoLists.Add(clientToDoList);
            _dbContext.SaveChanges();
            return _dbContext.ToDoLists.AsQueryable().FirstOrDefault(tdl => tdl.Id == toDoList.Id);
        }
    }
}
