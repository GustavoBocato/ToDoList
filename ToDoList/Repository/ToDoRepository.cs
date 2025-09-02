using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public class TodoRepository
    {
        private readonly ToDoDbContext _dbContext;

        public TodoRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Client? GetClientByEmail(string email)
        {
            return _dbContext.Clients.AsQueryable().FirstOrDefault(c => c.Email == email);
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
            return _dbContext.TodoLists.FirstOrDefault(tdl => tdl.Id == toDoList.Id);
        }

        public IEnumerable<TodoList> GetTodoListsByClientId(Guid clientId)
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

        public void SaveDbChanges()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<TodoItem> GetTodoItemsByTodoListId(Guid id)
        {
            return _dbContext.TodoItems.Where(tdi => tdi.IdTodolist == id)
                .ToList();
        }

        public IEnumerable<Client> GetClientsFromTodoList(Guid todoListId) 
        {
            return _dbContext.ClientTodoLists
                .Where(ctl => ctl.IdTodolist == todoListId)
                .Join
                (
                    _dbContext.Clients,
                    ctl => ctl.IdClient,
                    c => c.Id,
                    (ctl, c) => c
                )
                .ToList();
        }

        public TEntity? GetById<TEntity>(Guid id) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Post<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).Reload();

            return entity;
        }

        public void DeleteById<TEntity>(Guid id) where TEntity: class
        {
            var entity = GetById<TEntity>(id);

            if(entity is not null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}
