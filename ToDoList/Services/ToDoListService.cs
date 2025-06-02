using ToDoListApp.Models;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IClientToDoListRepository _clientToDoListRepository;
        public ToDoListService(IToDoListRepository toDoListRepository, IClientToDoListRepository clientToDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
            _clientToDoListRepository = clientToDoListRepository;
        }
        public int create(ToDoList toDoList) 
        {
            _toDoListRepository.create(toDoList);

        }
    }
}
