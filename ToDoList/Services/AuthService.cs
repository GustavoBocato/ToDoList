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

        public bool IsUserOwnerOfTodolist(Guid userId, Guid todolistId)
        {
            var clientTodolist = _todoRepository.GetClientTodolistAsync(userId, todolistId);

            if (clientTodolist is null) return false;

            return clientTodolist.IsOwner;
        }

        public bool CanUserPostClientTodolist(Guid userId, Guid todolistId)
        {
            return IsUserOwnerOfTodolist(userId, todolistId);
        }

        public bool CanUserDeleteClientTodolist(Guid userId, Guid clientTodolistId)
        {
            var relationshipToBeDeleted = _todoRepository.GetByIdAsync<ClientTodoList>(clientTodolistId);

            return userId == relationshipToBeDeleted.IdClient ||
                IsUserOwnerOfTodolist(userId, relationshipToBeDeleted.IdTodolist);
        }

        public bool IsUserIncludedOnAList(Guid userId, Guid todolistId) 
        {
            var clientTodolist = _todoRepository.GetClientTodolistAsync(userId, todolistId);

            if (clientTodolist is null) return false;

            return true;
        }
    }
}
