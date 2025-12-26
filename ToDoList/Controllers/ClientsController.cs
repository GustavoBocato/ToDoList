using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models.DTOs;
using TodoListApp.Services;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;
using ToDoListApp.Utils;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly JwtTokenService _jwtTokenService;

        public ClientsController(ITodoService toDoService,
            JwtTokenService jwtTokenService)
        {
            _todoService = toDoService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public async Task<ActionResult> PostClient(PostClientDTO clientDTO)
        {
            if (!await _todoService.ValidateClientRegistration(clientDTO))
                return BadRequest("O email do cliente a ser registrado j� foi cadastrado no sistema.");

            var client = await _todoService.Post<Client, PostClientDTO>(clientDTO);
            return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            try
            {
                var client = await _todoService.ValidateLogin(email, password);
                return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("me")]
        public async Task<ActionResult> Patch(PatchClientDTO patchClientDTO)
        {
            var clientId = GetClientIdFromUser();
            var newEmail = patchClientDTO.Email;

            if (newEmail is not null && await _todoService.ClientEmailAlreadyTaken(newEmail))
            {
                return BadRequest("O email a ser registrado j� pertence a um usu�rio.");
            }

            var result = await _todoService.Patch<Client, PatchClientDTO>(clientId, patchClientDTO);

            if (result is null)
            {
                return NotFound("Cliente a ser modificado n�o foi encontrado nas nossas bases.");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<ActionResult> Delete()
        {
            var clientId = GetClientIdFromUser();
            await _todoService.DeleteById<Client>(clientId);
            return Ok("O registro de cliente foi deletado com sucesso.");
        }
    }
}
