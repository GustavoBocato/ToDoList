using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TodoListApp.Repositories;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository toDoRepository, IMapper mapper)
        {
            _todoRepository = toDoRepository;
            _mapper = mapper;
        }

        public async Task<bool> ValidateClientRegistration(PostClientDTO client)
        {
            return !await ClientEmailAlreadyTaken(client.Email);
        }

        public async Task<bool> ClientEmailAlreadyTaken(string email)
        {
            if (await _todoRepository.GetClientByEmailAsync(email) is not null)
                return true;

            return false;
        }

        public async Task<Client> ValidateLogin(string email, string password)
        {
            Client client = await _todoRepository.GetClientByEmailAsync(email);

            if (client is null)
                throw new ArgumentException("O email entrado não consta na nossa base de dados.");

            var hasher = new PasswordHasher<Client>();
            var passwordVerificationResult = hasher.VerifyHashedPassword(client, client.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new ArgumentException("A senha entrada não corresponde a senha correta.");

            return client;
        }

        public async Task<TodoList> CreateToDoList(PostTodoListDTO toDoListDTO, Guid clientId)
        {
            var toDoList = _mapper.Map<TodoList>(toDoListDTO);

            return await _todoRepository.CreateToDoListAsync(toDoList, clientId);
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsByClientId(Guid clientId)
        {
            return await _todoRepository.GetTodoListsByClientIdAsync(clientId);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListId(Guid id)
        {
            return await _todoRepository.GetTodoItemsByTodoListIdAsync(id);
        }

        public async Task<IEnumerable<Client>> GetClientsFromTodoList(Guid todoListId)
        {
            return await _todoRepository.GetClientsFromTodoListAsync(todoListId);
        }

        public async Task<TEntity?> Patch<TEntity, TPatchDTO>(Guid id, TPatchDTO patchDTO) where TEntity : class
        {
            var entity = await _todoRepository.GetByIdAsync<TEntity>(id);

            if (entity is not null)
            {
                _mapper.Map(patchDTO, entity);
                await _todoRepository.SaveDbChangesAsync();
            }

            return entity;
        }

        public async Task<TEntity> Post<TEntity, TEntityDTO>(TEntityDTO entityDTO) where TEntity : class
        {
            var entity = _mapper.Map<TEntity>(entityDTO);

            return await _todoRepository.PostAsync<TEntity>(entity);
        }

        public async Task<TEntity?> GetById<TEntity>(Guid id) where TEntity : class
        {
            return await _todoRepository.GetByIdAsync<TEntity>(id);
        }

        public async Task DeleteById<TEntity>(Guid id) where TEntity : class
        {
            await _todoRepository.DeleteByIdAsync<TEntity>(id);
        }

        public async Task<bool> EntityExistsAsync<TEntity>(Guid id) where TEntity : class
        {
            var entity = await _todoRepository.GetByIdAsync<TEntity>(id);

            return entity is not null;
        }
    }
}
