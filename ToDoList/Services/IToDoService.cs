using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IToDoService
    {
        public void ValidateClientRegistration(Client client);
        public void RegisterClient(Client client);
        public Client ValidateLogin(string email, string password);
        public void CheckIfClientExists(Guid clientId);
        public ToDoList CreateToDoList(ToDoList toDoList, Guid clientId);
    }
}