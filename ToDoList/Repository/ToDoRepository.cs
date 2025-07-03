using AutoMapper;
using ToDoListApp.Data;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Client? CreateClient(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();

            return GetClientById(client.Id);
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

        public IEnumerable<ToDoList> GetToDoListsByClientId(Guid clientId)
        {
            return _dbContext.ClientToDoLists
                .Where(ctdl => ctdl.IdClient == clientId)
                .Join(_dbContext.ToDoLists,
                ctdl => ctdl.IdToDoList,
                tdl => tdl.Id,
                (ctdl, tdl) => tdl)
                .ToList();
        }
    }
}
