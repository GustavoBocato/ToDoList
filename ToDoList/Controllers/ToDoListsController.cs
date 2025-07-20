using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;
using ToDoListApp.Utils;

namespace ToDoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsController : BaseController
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
    }
}
