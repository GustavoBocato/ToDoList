using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public interface IClientRepository
    {

        public void insertClient(Client client);
        public Client GetClientByEmail(string email);

    }
}
