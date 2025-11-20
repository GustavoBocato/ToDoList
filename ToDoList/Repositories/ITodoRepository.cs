using ToDoListApp.Models;

namespace TodoListApp.Repositories
{
    public interface ITodoRepository
    {
        public Task<Client?> GetClientByEmailAsync(string email);
        public Task<TodoList> CreateToDoListAsync(TodoList toDoList, Guid clientId);
        public Task<IEnumerable<TodoList>> GetTodoListsByClientIdAsync(Guid clientId);
        public Task<ClientTodoList?> GetClientTodolistAsync(Guid clientId, Guid todolistId);
        public Task SaveDbChangesAsync();
        public Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListIdAsync(Guid id);
        public Task<IEnumerable<Client>> GetClientsFromTodoListAsync(Guid todoListId);
        public Task<TEntity?> GetByIdAsync<TEntity>(Guid id) where TEntity : class;
        public Task<TEntity> PostAsync<TEntity>(TEntity entity) where TEntity : class;
        public Task DeleteByIdAsync<TEntity>(Guid id) where TEntity : class;

    }
}
