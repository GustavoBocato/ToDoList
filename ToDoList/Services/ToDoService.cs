using Microsoft.AspNetCore.Identity;
using ToDoListApp.Models;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly PasswordHasher<Client> _passwordHasher = new();

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public void ValidateClientRegistration(Client client)
        {
            if (_toDoRepository.GetClientByEmail(client.Email) is not null)
                throw new ArgumentException("Um usuário com esse email já foi cadastrado.");
        }

        public void RegisterClient(Client client)
        {
            client.Password = _passwordHasher.HashPassword(client, client.Password);

            _toDoRepository.InsertClient(client);
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

        public ToDoList CreateToDoList(ToDoList toDoList, Guid clientId)
        {
            return _toDoRepository.CreateToDoList(toDoList, clientId);
        }
    }
}
