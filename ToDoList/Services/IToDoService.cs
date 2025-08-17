using TodoListApp.Models.DTOs;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Services
{
    public interface ITodoService
    {
        public bool ValidateClientRegistration(PostClientDTO client);
        public Client CreateClient(PostClientDTO clientDTO);
        public Client ValidateLogin(string email, string password);
        public void CheckIfClientExists(Guid clientId);
        public TodoList CreateToDoList(PostTodoListDTO toDoListDTO, Guid clientId);
        public IEnumerable<TodoList> GetToDoListsByClientId(Guid clientId);
        public ClientTodoList PostClientToDoList(PostClientTodoListDTO clientTodolistDTO);
        public void DeleteClientToDoList(Guid id);
        public void DeleteTodoListById(Guid id);
        public void PatchTodoList(Guid id, PatchTodoListDTO todolist);
    }
}