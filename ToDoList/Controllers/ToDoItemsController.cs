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
    public class TodoItemsController : BaseController
    {
        private readonly ITodoService _toDoService;

        public TodoItemsController(ITodoService service)
        {
            _toDoService = service;
        }

        [HttpPost]
        public ActionResult PostToDoItem(Guid toDoListId, TodoItem toDoItem) 
        {
            return Ok();
        }
    }
}
