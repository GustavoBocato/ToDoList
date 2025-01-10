using ToDoListApp.Models;

namespace ToDoListApp.Repository
{
    public interface IToDoListRepository
    {
        public void create(ToDoList toDoList);
    }
}
