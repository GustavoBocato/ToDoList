using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Repository
{
    public interface ITodoRepository
    {
        public Client CreateClient(Client client);
        public Client? GetClientByEmail(string email);
        public Client? GetClientById(Guid clientId);
        public Todolist CreateToDoList(Todolist toDoList, Guid clientId);
        public IEnumerable<Todolist> GetToDoListsByClientId(Guid clientId);
        public ClientTodolist? GetClientTodolist(Guid clientId, Guid todolistId);
        public ClientTodolist PostClientTodolist(ClientTodolist clientTodolist);
        public void DeleteClientTodolist(Guid id);
        public ClientTodolist? GetClientTodolistById(Guid id);
        public void DeleteTodoListById(Guid id);
    }
}