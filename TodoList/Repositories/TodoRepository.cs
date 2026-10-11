using Microsoft.EntityFrameworkCore;
using TodoListApp.Repositories;
using TodoListApp.Data;
using TodoListApp.Models;

namespace TodoListApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client?> GetClientByEmailAsync(string email)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<TodoList> CreateTodoListAsync(TodoList todoList, Guid clientId)
        {
            var clientTodoList = new ClientTodoList()
            {
                IdClient = clientId,
                IdTodoList = todoList.Id,
                IsOwner = true
            };

            _dbContext.TodoLists.Add(todoList);
            _dbContext.ClientTodoLists.Add(clientTodoList);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.TodoLists.FirstOrDefaultAsync(tdl => tdl.Id == todoList.Id);
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsByClientIdAsync(Guid clientId)
        {
            return await _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.IdClient == clientId)
                .Join(_dbContext.TodoLists,
                ctdl => ctdl.IdTodoList,
                tdl => tdl.Id,
                (ctdl, tdl) => tdl)
                .ToListAsync();
        }

        public async Task<ClientTodoList?> GetClientTodoListAsync(Guid clientId, Guid todoListId)
        {
            return await _dbContext.ClientTodoLists
                .Where(ctdl => ctdl.IdClient == clientId && ctdl.IdTodoList == todoListId)
                .FirstOrDefaultAsync();
        }

        public async Task SaveDbChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListIdAsync(Guid id)
        {
            return await _dbContext.TodoItems.Where(tdi => tdi.IdTodoList == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsFromTodoListAsync(Guid todoListId) 
        {
            return await _dbContext.ClientTodoLists
                .Where(ctl => ctl.IdTodoList == todoListId)
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
