using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models.DTOs;
using ToDoListApp.Models;
using ToDoListApp.Services;
using ToDoListApp.Utils;

namespace ToDoListApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseController
    {
        private readonly TodoService _todoService;
        private readonly AuthService _authService;

        public TodoItemsController(TodoService service, AuthService authService)
        {
            _todoService = service;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PostTodoItemDTO todoItemDTO)
        {
            var clientId = GetClientIdFromUser();
            var todoListId = todoItemDTO.IdTodolist;

            if (!await _authService.IsUserOwnerOfTodolist(clientId, todoListId))
            {
                return Unauthorized("O usuário não é dono da lista de afazeres para poder criar itens nela.");
            }

            return Ok(await _todoService.Post<TodoItem, PostTodoItemDTO>(todoItemDTO));
        }

        [HttpGet]
        public async Task<ActionResult> GetTodoItemsByTodoListId(Guid todoListId)
        {
            var clientId = GetClientIdFromUser();

            if (!await _authService.IsUserIncludedOnAList(clientId, todoListId))
                return Unauthorized("O usuário não tem permissão para ver os itens dessa lista de afazeres.");

            return Ok(await _todoService.GetTodoItemsByTodoListId(todoListId));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid todoItemId)
        {
            var clientId = GetClientIdFromUser();
            var todoItem = await _todoService.GetById<TodoItem>(todoItemId);

            if (todoItem is null)
                return NotFound("Item a ser deletado não existe no banco de dados.");

            var todoListId = todoItem.IdTodolist;

            if (!await _authService.IsUserOwnerOfTodolist(clientId, todoListId))
                return Unauthorized("Usuário não tem permissão para deletar itens.");

            await _todoService.DeleteById<TodoItem>(todoItemId);
            return Ok("Item deletado com sucesso.");
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(Guid todoItemId, [FromBody] PatchTodoItemDTO patchTodoItemDTO)
        {
            var clientId = GetClientIdFromUser();
            var todoItem = await _todoService.GetById<TodoItem>(todoItemId);

            if (todoItem is null)
                return NotFound("Item a ser atualizado não existe no banco de dados.");

            var todoListId = todoItem.IdTodolist;

            if (!await _authService.IsUserOwnerOfTodolist(clientId, todoListId))
                return Unauthorized("Usuário não tem permissão para deletar itens.");

            var result = await _todoService.Patch<TodoItem, PatchTodoItemDTO>(todoItemId, patchTodoItemDTO);

            return Ok(result);
        }
    }
}
