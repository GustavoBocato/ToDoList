using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IClientToDoListService
    {
        public void validatePostToDoList(string ownersEmail, ToDoList toDoList);
    }
}
