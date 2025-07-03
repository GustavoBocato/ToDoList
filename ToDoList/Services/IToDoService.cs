using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Services
{
    public interface IToDoService
    {
        public void ValidateClientRegistration(ClientDTO client);
        public Client CreateClient(ClientDTO clientDTO);
        public Client ValidateLogin(string email, string password);
        public void CheckIfClientExists(Guid clientId);
        public ToDoList CreateToDoList(ToDoListDTO toDoListDTO, Guid clientId);
        public IEnumerable<ToDoList> GetToDoListsByClientId(Guid clientId);
    }
}