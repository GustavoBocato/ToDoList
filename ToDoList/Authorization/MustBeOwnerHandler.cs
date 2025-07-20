using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ToDoListApp.Data;

namespace ToDoListApp.Authorization
{
    public class MustBeOwnerHandler : IAuthorizationHandler
    {
        private readonly ToDoDbContext toDoDbContext;

        public MustBeOwnerHandler(ToDoDbContext toDoDbContext)
        {
            this.toDoDbContext = toDoDbContext;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var sub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;



            return Task.CompletedTask;
        }
    }
}
