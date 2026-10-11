using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models.DTOs;
using TodoListApp.Services;
using TodoListApp.Models;
using TodoListApp.Utils;

namespace TodoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly IAuthService _authService;

        public TodoListsController(ITodoService todoService, IAuthService authService)
        {
            _todoService = todoService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> PostTodoList(PostTodoListDTO todoListDTO)
        {
            var clientId = GetClientIdFromUser();

            if (!await _todoService.EntityExists<Client>(clientId)) return BadRequest("O usuário a criar a lista de " +
                "afazeres não existe na nossa base de dados.");

            return Ok(await _todoService.CreateTodoList(todoListDTO, clientId));
        }

        [HttpGet]
        public async Task<ActionResult> GetTodoListsFromUser()
        {
            var clientId = GetClientIdFromUser();
            return Ok(await _todoService.GetTodoListsByClientId(clientId));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoList(Guid id) 
        {
            var clientId = GetClientIdFromUser();

            if(!await _authService.IsUserOwnerOfTodoList(clientId, id))
                return Unauthorized("Usuário não tem permissão para deletar lista de afazeres.");

            await _todoService.DeleteById<TodoList>(id);
            return Ok("Lista de afazeres deletada com successo.");
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(Guid id, [FromBody] PatchTodoListDTO todoList)
        {
            var clientId = GetClientIdFromUser();

            if (!await _authService.IsUserOwnerOfTodoList(clientId, id))
                return Unauthorized("Usuário não pode modificar lista de afazeres de que não é dono.");

            var result = await _todoService.Patch<TodoList, PatchTodoListDTO>(id, todoList);

            if (result == null)
            {
                return NotFound("Não se pode encontrar uma lista" +
                " de afazeres com esse id para se atualizar.");
            }

            return Ok(result);
        }

        [HttpGet("{todoListId}")]
        public async Task<ActionResult> GetClientsFromTodoList(Guid todoListId)
        {
            // analisar mover esse endpoint para a controller de clients
            var clientId = GetClientIdFromUser();

            if(!await _authService.IsUserIncludedOnAList(clientId, todoListId))
                return Unauthorized("Usuário não pode ver os outros incluidos em uma lista a qual não pertence.");

            if (!await _todoService.EntityExists<TodoList>(todoListId)) return NotFound("Lista de afazeres não existe" +
                " nas nosssas bases de dados.");

            return Ok(await _todoService.GetClientsFromTodoList(todoListId));
        }
    }
}
