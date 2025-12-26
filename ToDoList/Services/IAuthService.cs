namespace TodoListApp.Services
{
    public interface IAuthService
    {
        public  Task<bool> IsUserOwnerOfTodolist(Guid userId, Guid todolistId);
        public  Task<bool> CanUserPostClientTodolist(Guid userId, Guid todolistId);
        public  Task<bool> CanUserDeleteClientTodolist(Guid userId, Guid clientTodolistId);
        public  Task<bool> IsUserIncludedOnAList(Guid userId, Guid todolistId);
    }
}
