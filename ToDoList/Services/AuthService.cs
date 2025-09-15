using ToDoListApp.Models;
using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class AuthService
    {
        private readonly TodoRepository _todoRepository;

        public AuthService(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<bool> IsUserOwnerOfTodolist(Guid userId, Guid todolistId)
        {
            var clientTodolist = await _todoRepository.GetClientTodolistAsync(userId, todolistId);

            if (clientTodolist is null) return false;

            return clientTodolist.IsOwner;
        }

        public async Task<bool> CanUserPostClientTodolist(Guid userId, Guid todolistId)
        {
            return await IsUserOwnerOfTodolist(userId, todolistId);
        }

        public async Task<bool> CanUserDeleteClientTodolist(Guid userId, Guid clientTodolistId)
        {
            var relationshipToBeDeleted = await _todoRepository.GetByIdAsync<ClientTodoList>(clientTodolistId);

            return userId == relationshipToBeDeleted.IdClient ||
                await IsUserOwnerOfTodolist(userId, relationshipToBeDeleted.IdTodolist);
        }

        public async Task<bool> IsUserIncludedOnAList(Guid userId, Guid todolistId) 
        {
            var clientTodolist = await _todoRepository.GetClientTodolistAsync(userId, todolistId);

            if (clientTodolist is null) return false;

            return true;
        }
    }
}
