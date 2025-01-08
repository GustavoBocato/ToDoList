using Microsoft.EntityFrameworkCore;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ToDoListDbContext _toDoListDbContext;

        public ClientRepository(ToDoListDbContext toDoListDbContext) {

            _toDoListDbContext = toDoListDbContext; 
        
        }

        public void insertClient(Client client)
        {
            _toDoListDbContext.clients.Add(client);
            _toDoListDbContext.SaveChanges();
        }

        public Client GetClientByEmail(string email)
        {
            return _toDoListDbContext.clients.AsQueryable().FirstOrDefault(c => c.email == email);
        }

        public void updateClient(Client client) 
        {
            _toDoListDbContext.clients.Update(client);
            _toDoListDbContext.SaveChanges();
        }
    }
}
