using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly PasswordHasher<Client> _passwordHasher = new();
        private readonly IMapper _mapper;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
        }

        public void ValidateClientRegistration(ClientDTO client)
        {
            if (_toDoRepository.GetClientByEmail(client.Email) is not null)
                throw new ArgumentException("Um usuário com esse email já foi cadastrado.");
        }

        public Client CreateClient(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);

            client.Password = _passwordHasher.HashPassword(client, client.Password);

            return _toDoRepository.CreateClient(client);
        }

        public Client ValidateLogin(String email, String password)
        {
            Client client = _toDoRepository.GetClientByEmail(email);

            if (client is null)
                throw new ArgumentException("O email entrado não consta na nossa base de dados.");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(client, client.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new ArgumentException("A senha entrada não corresponde a senha correta.");

            return client;
        }

        public void CheckIfClientExists(Guid clientId)
        {
            var client = _toDoRepository.GetClientById(clientId);

            if (client is null)
                throw new ArgumentException("Não existe cliente cujo id corresponde ao requisitante.");
        }

        public ToDoList CreateToDoList(ToDoListDTO toDoListDTO, Guid clientId)
        {
            var toDoList = _mapper.Map<ToDoList>(toDoListDTO);

            return _toDoRepository.CreateToDoList(toDoList, clientId);
        }

        public IEnumerable<ToDoList> GetToDoListsByClientId(Guid clientId) 
        {
            return _toDoRepository.GetToDoListsByClientId(clientId);
        }
    }
}
