using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoListsController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpPost]
        public ActionResult PostToDoList(ToDoListDTO toDoListDTO)
        {
            try
            {
                var sub = User.FindFirst("sub")?.Value;

                if (!Guid.TryParse(sub, out var clientId))
                    throw new ArgumentException("Id do usuário está mal formado.");

                _toDoService.CheckIfClientExists(clientId);
                var createdToDoList = _toDoService.CreateToDoList(toDoListDTO, clientId);

                return Ok(createdToDoList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/clients/{clientId}/todolists")]
        public ActionResult GetToDoList(Guid clientId) 
        {
            return Ok(_toDoService.GetToDoListsByClientId(clientId));
        }
    }
}
