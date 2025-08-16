namespace ToDoListApp.Services
{
    public interface IAuthService
    {
        public bool CanUserPostClientTodolist(Guid userId, Guid todolistId);
        public bool CanUserDeleteClientTodolist(Guid userId, Guid clientTodolistId);
    }
}