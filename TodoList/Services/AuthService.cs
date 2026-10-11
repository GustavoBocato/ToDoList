using TodoListApp.Models;
using TodoListApp.Repositories;
using TodoListApp.Services;

namespace TodoListApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITodoRepository _todoRepository;

        public AuthService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<bool> IsUserOwnerOfTodoList(Guid userId, Guid todoListId)
        {
            var clientTodoList = await _todoRepository.GetClientTodoListAsync(userId, todoListId);

            if (clientTodoList is null) return false;

            return clientTodoList.IsOwner;
        }

        public async Task<bool> CanUserPostClientTodoList(Guid userId, Guid todoListId)
        {
            return await IsUserOwnerOfTodoList(userId, todoListId);
        }

        public async Task<bool> CanUserDeleteClientTodoList(Guid userId, Guid clientTodoListId)
        {
            var relationshipToBeDeleted = await _todoRepository.GetByIdAsync<ClientTodoList>(clientTodoListId);

            return userId == relationshipToBeDeleted.IdClient ||
                await IsUserOwnerOfTodoList(userId, relationshipToBeDeleted.IdTodoList);
        }

        public async Task<bool> IsUserIncludedOnAList(Guid userId, Guid todoListId) 
        {
            var clientTodoList = await _todoRepository.GetClientTodoListAsync(userId, todoListId);

            if (clientTodoList is null) return false;

            return true;
        }
    }
}
