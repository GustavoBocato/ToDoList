using ToDoListApp.Models;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class ClientToDoListService : IClientToDoListService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IToDoListRepository _toDoListRepository;

        public ClientToDoListService(IClientRepository clientRepository, IToDoListRepository toDoListRepository) 
        {
            _clientRepository = clientRepository;
            _toDoListRepository = toDoListRepository;
        }
        public void validatePostToDoList(string ownersEmail, ToDoList toDoList)
        {
            if(_clientRepository.GetClientByEmail(ownersEmail) is null)
            {
                throw new InvalidOperationException("O dono da lista não existe na nossa base de dados.");
            }
        }
    }
}
