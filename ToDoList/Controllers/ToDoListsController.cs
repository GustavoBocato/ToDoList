using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models.DTOs;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;
using ToDoListApp.Utils;

namespace ToDoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : BaseController
    {
        private readonly ITodoService _toDoService;

        public TodoListsController(ITodoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpPost]
        public ActionResult PostToDoList(PostTodoListDTO toDoListDTO)
        {
            try
            {
                var clientId = GetClientIdFromUser();

                _toDoService.CheckIfClientExists(clientId);
                var createdToDoList = _toDoService.CreateToDoList(toDoListDTO, clientId);

                return Ok(createdToDoList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetToDoLists()
        {
            try
            {
                var clientId = GetClientIdFromUser();
                return Ok(_toDoService.GetToDoListsByClientId(clientId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteTodoList(Guid id) 
        {
            _toDoService.DeleteTodoListById(id);
            return Ok("Lista de afazeres deletada com successo.");
        }

        [HttpPatch]
        public IActionResult PatchTodoList(Guid id, [FromBody] PatchTodoListDTO todolist)
        {
            _toDoService.PatchTodoList(id, todolist);
            return Ok();
        }
    }
}
