using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IToDoListService
    {
        public void create(ToDoList toDoList);
    }
}
