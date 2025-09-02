using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models.DTOs;
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
        private readonly TodoService _todoService;
        private readonly JwtTokenService _jwtTokenService;

        public ClientsController(TodoService toDoService,
            JwtTokenService jwtTokenService)
        {
            _todoService = toDoService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public ActionResult PostClient(PostClientDTO clientDTO)
        {
            if (!_todoService.ValidateClientRegistration(clientDTO))
                return BadRequest("O email do cliente a ser registrado já foi cadastrado no sistema.");

            var client = _todoService.Post<Client, PostClientDTO>(clientDTO);
            return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
        }

        [HttpPost("login")]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var client = _todoService.ValidateLogin(email, password);
                return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        public ActionResult Patch(PatchClientDTO patchClientDTO)
        {
            var clientId = GetClientIdFromUser();
            var newEmail = patchClientDTO.Email;

            if (newEmail is not null && _todoService.ClientEmailAlreadyTaken(newEmail))
            {
                return BadRequest("O email a ser registrado já pertence a um usuário.");
            }

            var result = _todoService.Patch<Client, PatchClientDTO>(clientId, patchClientDTO);

            if (result is null)
            {
                return NotFound("Cliente a ser modificado não foi encontrado nas nossas bases.");
            }

            return Ok(result);
        }
    }
}
