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
    public class ClientTodolistsController : BaseController
    {
        private readonly ITodoService _toDoService;
        private readonly IAuthService _authService;

        public ClientTodolistsController(ITodoService toDoService, IAuthService authService)
        {
            _toDoService = toDoService;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult Post(ClientTodolistDTO clientTodolistDTO)
        {
            if (!_authService.CanUserPostClientTodolist(GetClientIdFromUser(), clientTodolistDTO.IdTodolist))
                return Forbid("O usuário não é dono da lista de afazeres, logo não pode adcionar outros" +
                    " clientes a lista.");

            return Ok(_toDoService.PostClientToDoList(clientTodolistDTO));
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            if (!_authService.CanUserDeleteClientTodolist(GetClientIdFromUser(), id))
                return Forbid("O usuário não pode remover o cliente da lista de afazeres. Porque não é nem dono" +
                    " da lista e nem é o cliente a ser removido.");

            _toDoService.DeleteClientToDoList(id);

            return Ok();
        }
    }
}
