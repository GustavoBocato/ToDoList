using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class TodoService
    {
        private readonly TodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(TodoRepository toDoRepository, IMapper mapper)
        {
            _todoRepository = toDoRepository;
            _mapper = mapper;
        }

        public bool ValidateClientRegistration(PostClientDTO client)
        {
            return !ClientEmailAlreadyTaken(client.Email);
        }

        public bool ClientEmailAlreadyTaken(string email)
        {
            if (_todoRepository.GetClientByEmail(email) is not null)
                return true;

            return false;
        }

        public Client ValidateLogin(string email, string password)
        {
            Client client = _todoRepository.GetClientByEmail(email);

            if (client is null)
                throw new ArgumentException("O email entrado não consta na nossa base de dados.");

            var hasher = new PasswordHasher<Client>();
            var passwordVerificationResult = hasher.VerifyHashedPassword(client, client.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new ArgumentException("A senha entrada não corresponde a senha correta.");

            return client;
        }

        public TodoList CreateToDoList(PostTodoListDTO toDoListDTO, Guid clientId)
        {
            var toDoList = _mapper.Map<TodoList>(toDoListDTO);

            return _todoRepository.CreateToDoList(toDoList, clientId);
        }

        public IEnumerable<TodoList> GetTodoListsByClientId(Guid clientId)
        {
            return _todoRepository.GetTodoListsByClientId(clientId);
        }

        public IEnumerable<TodoItem> GetTodoItemsByTodoListId(Guid id)
        {
            return _todoRepository.GetTodoItemsByTodoListId(id);
        }

        public IEnumerable<Client> GetClientsFromTodoList(Guid todoListId)
        {
            return _todoRepository.GetClientsFromTodoList(todoListId);
        }

        public TEntity? Patch<TEntity, TPatchDTO>(Guid id, TPatchDTO patchDTO) where TEntity : class
        {
            var entity = _todoRepository.GetById<TEntity>(id);

            if (entity is not null)
            {
                _mapper.Map(patchDTO, entity);
                _todoRepository.SaveDbChanges();
            }

            return entity;
        }

        public TEntity Post<TEntity, TEntityDTO>(TEntityDTO entityDTO) where TEntity : class
        {
            var entity = _mapper.Map<TEntity>(entityDTO);

            return _todoRepository.Post<TEntity>(entity);
        }

        public TEntity? GetById<TEntity>(Guid id) where TEntity : class
        {
            return _todoRepository.GetById<TEntity>(id);
        }

        public void DeleteById<TEntity>(Guid id) where TEntity : class
        {
            _todoRepository.DeleteById<TEntity>(id);
        }

        public bool EntityExists<TEntity>(Guid id) where TEntity : class
        {
            var entity = _todoRepository.GetById<TEntity>(id);

            return entity is not null;
        }
    }
}
