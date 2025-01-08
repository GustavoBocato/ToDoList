using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IClientService
    {
        public void validateRegistration(String email);
        public bool emailAlreadyRegistered(String email);
        public void registerClient(Client client);
        public Client validateLogin(String email, String password);

    }
}
