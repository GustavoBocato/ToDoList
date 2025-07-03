using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Repository
{
    public interface IToDoRepository
    {
        public Client CreateClient(Client client);
        public Client? GetClientByEmail(string email);
        public Client? GetClientById(Guid clientId);
        public ToDoList CreateToDoList(ToDoList toDoList, Guid clientId);
        public IEnumerable<ToDoList> GetToDoListsByClientId(Guid clientId);
    }
}