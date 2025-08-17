using TodoListApp.Models.DTOs;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Repository
{
    public interface ITodoRepository
    {
        public Client CreateClient(Client client);
        public Client? GetClientByEmail(string email);
        public Client? GetClientById(Guid clientId);
        public TodoList CreateToDoList(TodoList toDoList, Guid clientId);
        public IEnumerable<TodoList> GetToDoListsByClientId(Guid clientId);
        public ClientTodoList? GetClientTodolist(Guid clientId, Guid todolistId);
        public ClientTodoList PostClientTodolist(ClientTodoList clientTodolist);
        public void DeleteClientTodolist(Guid id);
        public ClientTodoList? GetClientTodolistById(Guid id);
        public void DeleteTodoListById(Guid id);
        public void PatchTodoList(Guid id, PatchTodoListDTO todolist);
    }
}