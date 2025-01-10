using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public interface IClientToDoListRepository
    {
        public void create(ClientToDoList clientToDoList);
    }
}
