using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IToDoListService
    {
        public int create(ToDoList toDoList);
    }
}
