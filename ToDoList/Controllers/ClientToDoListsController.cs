using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Services;
using ToDoListApp.Utils;

namespace ToDoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientToDoListsController : BaseController
    {
        private readonly IToDoService _toDoService;
        public ClientToDoListsController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpPost()]
        public ActionResult PostClientToDoList(Guid clientToBeAddedId, Guid todolistId)
        {
            var ownerId = GetClientIdFromUser();


        }
    }
}
