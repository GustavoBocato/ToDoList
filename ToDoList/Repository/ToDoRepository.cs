using TodoListApp.Models.DTOs;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ToDoDbContext _dbContext;

        public TodoRepository(ToDoDbContext dbContext)
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

        public TodoList CreateToDoList(TodoList toDoList, Guid clientId)
        {
            var clientToDoList = new ClientTodoList()
            {
                IdClient = clientId,
                IdTodolist = toDoList.Id,
                IsOwner = true
            };

            _dbContext.TodoLists.Add(toDoList);
            _dbContext.ClientTodoLists.Add(clientToDoList);
            _dbContext.SaveChanges();
            return _dbContext.TodoLists.AsQueryable().FirstOrDefault(tdl => tdl.Id == toDoList.Id);
        }

        public IEnumerable<TodoList> GetToDoListsByClientId(Guid clientId)
        {
            return _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.IdClient == clientId)
                .Join(_dbContext.TodoLists,
                ctdl => ctdl.IdTodolist,
                tdl => tdl.Id,
                (ctdl, tdl) => tdl)
                .ToList();
        }

        public ClientTodoList? GetClientTodolist(Guid clientId, Guid todolistId)
        {
            return _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.IdClient == clientId && ctdl.IdTodolist == todolistId)
                .FirstOrDefault();
        }

        public ClientTodoList PostClientTodolist(ClientTodoList clientTodolist)
        {
            _dbContext.ClientTodoLists.Add(clientTodolist);
            _dbContext.SaveChanges();

            return GetClientTodolist(clientTodolist.IdClient, clientTodolist.IdTodolist);
        }

        public void DeleteClientTodolist(Guid id)
        {
            var clientTodolist = _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.Id == id)
                .FirstOrDefault();

            if (clientTodolist != null)
            {
                _dbContext.ClientTodoLists.Remove(clientTodolist);
                _dbContext.SaveChanges();
            }
        }

        public ClientTodoList? GetClientTodolistById(Guid id)
        {
            return _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.Id == id)
                .FirstOrDefault();
        }

        public void DeleteTodoListById(Guid id)
        {
            var todoList = _dbContext.TodoLists.Where(tdl => tdl.Id == id).FirstOrDefault();

            if (todoList != null)
            {
                _dbContext.TodoLists.Remove(todoList);
                _dbContext.SaveChanges();
            }
        }

        public TodoList? GetTodoListById(Guid id)
        {
            return _dbContext.TodoLists.Where(tdl => tdl.Id == id).FirstOrDefault();
        }

        public void SaveDbChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
