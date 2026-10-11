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
    public class TodoItemsController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly IAuthService _authService;

        public TodoItemsController(ITodoService service, IAuthService authService)
        {
            _todoService = service;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PostTodoItemDTO todoItemDTO)
        {
            var clientId = GetClientIdFromUser();
            var todoListId = todoItemDTO.IdTodoList;

            if (!await _authService.IsUserOwnerOfTodoList(clientId, todoListId))
            {
                return Unauthorized("O usuário não é dono da lista de afazeres para poder criar itens nela.");
            }

            return Ok(await _todoService.Post<TodoItem, PostTodoItemDTO>(todoItemDTO));
        }

        [HttpGet("{todoListId}")]
        public async Task<ActionResult> GetTodoItemsByTodoListId(Guid todoListId)
        {
            var clientId = GetClientIdFromUser();

            if (!await _authService.IsUserIncludedOnAList(clientId, todoListId))
                return Unauthorized("O usuário não tem permissão para ver os itens dessa lista de afazeres.");

            return Ok(await _todoService.GetTodoItemsByTodoListId(todoListId));
        }

        [HttpDelete("{todoItemId}")]
        public async Task<ActionResult> Delete(Guid todoItemId)
        {
            var clientId = GetClientIdFromUser();
            var todoItem = await _todoService.GetById<TodoItem>(todoItemId);

            if (todoItem is null)
                return NotFound("Item a ser deletado não existe no banco de dados.");

            var todoListId = todoItem.IdTodoList;

            if (!await _authService.IsUserOwnerOfTodoList(clientId, todoListId))
                return Unauthorized("Usuário não tem permissão para deletar itens.");

            await _todoService.DeleteById<TodoItem>(todoItemId);
            return Ok("Item deletado com sucesso.");
        }

        [HttpPatch("{todoItemId}")]
        public async Task<ActionResult> Patch(Guid todoItemId, [FromBody] PatchTodoItemDTO patchTodoItemDTO)
        {
            var clientId = GetClientIdFromUser();
            var todoItem = await _todoService.GetById<TodoItem>(todoItemId);

            if (todoItem is null)
                return NotFound("Item a ser atualizado não existe no banco de dados.");

            var todoListId = todoItem.IdTodoList;

            if (!await _authService.IsUserOwnerOfTodoList(clientId, todoListId))
                return Unauthorized("Usuário não tem permissão para deletar itens.");

            var result = await _todoService.Patch<TodoItem, PatchTodoItemDTO>(todoItemId, patchTodoItemDTO);

            return Ok(result);
        }
    }
}
