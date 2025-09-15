using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
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
        private readonly TodoService _todoService;
        private readonly AuthService _authService;

        public ClientTodolistsController(TodoService toDoService, AuthService authService)
        {
            _todoService = toDoService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PostClientTodoListDTO clientTodolistDTO)
        {
            if (!await _authService.CanUserPostClientTodolist(GetClientIdFromUser(), clientTodolistDTO.IdTodolist))
                return Forbid("O usuário não é dono da lista de afazeres, logo não pode adcionar outros" +
                    " clientes a lista.");

            return Ok(await _todoService.Post<ClientTodoList, PostClientTodoListDTO>(clientTodolistDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!await _authService.CanUserDeleteClientTodolist(GetClientIdFromUser(), id))
                return Forbid("O usuário não pode remover o cliente da lista de afazeres. Porque não é nem dono" +
                    " da lista e nem é o cliente a ser removido.");

            await _todoService.DeleteById<ClientTodoList>(id);

            return Ok();
        }
    }
}
