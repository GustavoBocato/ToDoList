using ToDoListApp.Repository;

namespace ToDoListApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITodoRepository _todoRepository;

        public AuthService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        private bool IsUserOwnerOfTodolist(Guid userId, Guid todolistId)
        {
            var clientTodolist = _todoRepository.GetClientTodolist(userId, todolistId);

            if (clientTodolist == null) return false;

            return clientTodolist.IsOwner;
        }

        public bool CanUserPostClientTodolist(Guid userId, Guid todolistId)
        {
            return IsUserOwnerOfTodolist(userId, todolistId);
        }

        public bool CanUserDeleteClientTodolist(Guid userId, Guid clientTodolistId)
        {
            var relationshipToBeDeleted = _todoRepository.GetClientTodolistById(clientTodolistId);

            return userId == relationshipToBeDeleted.IdClient ||
                IsUserOwnerOfTodolist(userId, relationshipToBeDeleted.IdTodolist);
        }
    }
}
