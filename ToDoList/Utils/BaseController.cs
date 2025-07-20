using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ToDoListApp.Utils
{
    public class BaseController : ControllerBase
    {
        protected Guid GetClientIdFromUser()
        {
            var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(sub, out var clientId))
                throw new ArgumentException("Id do usuário está mal formado.");

            return clientId;
        }
    }
}
