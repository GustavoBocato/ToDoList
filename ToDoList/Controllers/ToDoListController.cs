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
        private readonly IClientService _clientService;

        public ToDoListController(IToDoListService toDoListService, IClientService clientService) 
        {
            _toDoListService = toDoListService;
            _clientService = clientService;
        }

        [HttpPost("create")]
        [Authorize]
        public ActionResult postToDoList(ToDoList toDoList, string ownersEmail)
        {
            try
            {
                var ownersId = _clientService.validateOwner(ownersEmail);
                _toDoListService.create(toDoList);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
