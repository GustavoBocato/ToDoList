using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;

namespace ToDoListApp.Services
{
    public interface ITodoService
    {
        public bool ValidateClientRegistration(ClientDTO client);
        public Client CreateClient(ClientDTO clientDTO);
        public Client ValidateLogin(string email, string password);
        public void CheckIfClientExists(Guid clientId);
        public Todolist CreateToDoList(TodolistDTO toDoListDTO, Guid clientId);
        public IEnumerable<Todolist> GetToDoListsByClientId(Guid clientId);
        public ClientTodolist PostClientToDoList(ClientTodolistDTO clientTodolistDTO);
        public void DeleteClientToDoList(Guid id);
        public void DeleteTodoListById(Guid id);
    }
}