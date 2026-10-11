using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.Models;
using TodoListApp.Models.DTOs;
using TodoListApp.Utils;

namespace TodoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientTodoListsController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly IAuthService _authService;

        public ClientTodoListsController(ITodoService todoService, IAuthService authService)
        {
            _todoService = todoService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PostClientTodoListDTO clientTodoListDTO)
        {
            if (!await _authService.CanUserPostClientTodoList(GetClientIdFromUser(), clientTodoListDTO.IdTodoList))
                return Forbid("O usuário não é dono da lista de afazeres, logo não pode adcionar outros" +
                    " clientes a lista.");

            return Ok(await _todoService.Post<ClientTodoList, PostClientTodoListDTO>(clientTodoListDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!await _authService.CanUserDeleteClientTodoList(GetClientIdFromUser(), id))
                return Forbid("O usuário não pode remover o cliente da lista de afazeres. Porque não é nem dono" +
                    " da lista e nem é o cliente a ser removido.");

            await _todoService.DeleteById<ClientTodoList>(id);

            return Ok();
        }
    }
}
