using ToDoListApp.Models.DTOs;
using ToDoListApp.Models;

namespace TodoListApp.Services
{
    public interface ITodoService
    {
        public Task<bool> ValidateClientRegistration(PostClientDTO client);
        public Task<bool> ClientEmailAlreadyTaken(string email);
        public  Task<Client> ValidateLogin(string email, string password);
        public  Task<TodoList> CreateToDoList(PostTodoListDTO toDoListDTO, Guid clientId);
        public  Task<IEnumerable<TodoList>> GetTodoListsByClientId(Guid clientId);
        public  Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListId(Guid id);
        public  Task<IEnumerable<Client>> GetClientsFromTodoList(Guid todoListId);
        public  Task<TEntity?> Patch<TEntity, TPatchDTO>(Guid id, TPatchDTO patchDTO) where TEntity : class;
        public  Task<TEntity> Post<TEntity, TEntityDTO>(TEntityDTO entityDTO) where TEntity : class;
        public  Task<TEntity?> GetById<TEntity>(Guid id) where TEntity : class;
        public  Task DeleteById<TEntity>(Guid id) where TEntity : class;
        public  Task<bool> EntityExists<TEntity>(Guid id) where TEntity : class;
    }
}
