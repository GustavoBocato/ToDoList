namespace TodoListApp.Services
{
    public interface IAuthService
    {
        public  Task<bool> IsUserOwnerOfTodoList(Guid userId, Guid todoListId);
        public  Task<bool> CanUserPostClientTodoList(Guid userId, Guid todoListId);
        public  Task<bool> CanUserDeleteClientTodoList(Guid userId, Guid clientTodoListId);
        public  Task<bool> IsUserIncludedOnAList(Guid userId, Guid todoListId);
    }
}
