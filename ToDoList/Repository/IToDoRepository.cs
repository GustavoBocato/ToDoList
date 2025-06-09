using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public interface IToDoRepository
    {
        public void InsertClient(Client client);
        public Client GetClientByEmail(string email);
        public Client GetClientById(Guid clientId);
        public ToDoList CreateToDoList(ToDoList toDoList, Guid clientId);
    }
}