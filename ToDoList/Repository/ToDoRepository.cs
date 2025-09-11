using Microsoft.EntityFrameworkCore;
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

        public async Task<Client?> GetClientByEmailAsync(string email)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<TodoList> CreateToDoListAsync(TodoList toDoList, Guid clientId)
        {
            var clientToDoList = new ClientTodoList()
            {
                IdClient = clientId,
                IdTodolist = toDoList.Id,
                IsOwner = true
            };

            _dbContext.TodoLists.Add(toDoList);
            _dbContext.ClientTodoLists.Add(clientToDoList);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.TodoLists.FirstOrDefaultAsync(tdl => tdl.Id == toDoList.Id);
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsByClientIdAsync(Guid clientId)
        {
            return await _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.IdClient == clientId)
                .Join(_dbContext.TodoLists,
                ctdl => ctdl.IdTodolist,
                tdl => tdl.Id,
                (ctdl, tdl) => tdl)
                .ToListAsync();
        }

        public async Task<ClientTodoList?> GetClientTodolistAsync(Guid clientId, Guid todolistId)
        {
            return await _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.IdClient == clientId && ctdl.IdTodolist == todolistId)
                .FirstOrDefaultAsync();
        }

        public async Task SaveDbChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListIdAsync(Guid id)
        {
            return await _dbContext.TodoItems.Where(tdi => tdi.IdTodolist == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsFromTodoListAsync(Guid todoListId) 
        {
            return await _dbContext.ClientTodoLists
                .Where(ctl => ctl.IdTodolist == todoListId)
                .Join
                (
                    _dbContext.Clients,
                    ctl => ctl.IdClient,
                    c => c.Id,
                    (ctl, c) => c
                )
                .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(Guid id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> PostAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(entity).ReloadAsync();

            return entity;
        }

        public async Task DeleteByIdAsync<TEntity>(Guid id) where TEntity: class
        {
            var entity = await GetByIdAsync<TEntity>(id);

            if(entity is not null)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
