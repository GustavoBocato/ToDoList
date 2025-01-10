using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService) 
        {
            _toDoListService = toDoListService;
        }

        [HttpPost("create")]
        [Authorize]
        public ActionResult postToDoList(ToDoList toDoList)
        {
            _toDoListService.create(toDoList);
            return Ok();
        }

    }
}
