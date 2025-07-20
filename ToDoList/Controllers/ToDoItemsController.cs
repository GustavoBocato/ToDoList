using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using ToDoListApp.Services;
using ToDoListApp.Utils;

namespace ToDoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : BaseController
    {
        private readonly IToDoService _toDoService;

        public ToDoItemsController(IToDoService service)
        {
            _toDoService = service;
        }

        [HttpPost]
        public ActionResult PostToDoItem(Guid toDoListId, ToDoItem toDoItem) 
        {
            
        }
    }
}
