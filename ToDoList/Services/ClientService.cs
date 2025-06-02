using ToDoListApp.Models;
using ToDoListApp.Repository;
using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;
        private readonly PasswordHasher<Client> _passwordHasher = new();

        public ClientService(IClientRepository clientRepository) {
            _clientRepository = clientRepository;
        }

        public void validateRegistration(String email)
        {
            if (emailAlreadyRegistered(email)) 
                throw new ArgumentException("Um usuário com esse email já foi cadastrado.");
        }

        public bool emailAlreadyRegistered(String email)
        {
            if(_clientRepository.GetClientByEmail(email) is null) return false;
            
            return true;
        }

        public void registerClient(Client client)
        {
            client.password = _passwordHasher.HashPassword(client, client.password);

            _clientRepository.insertClient(client);
        }

        public Client validateLogin(String email, String password)
        {
            Client client = _clientRepository.GetClientByEmail(email);

            if (client is null)
                throw new ArgumentException("O email entrado não consta na nossa base de dados.");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(client, client.password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new ArgumentException("A senha entrada não corresponde a senha correta.");

            return client;
        }

        public int validateOwner(String ownersEmail)
        {
            var owner = _clientRepository.GetClientByEmail(ownersEmail);

            if (owner is null)
                throw new ArgumentException("O email do dono da lista não corresponde a nenhum email de um cliente.");

            return owner.id;
        }
    }
}
