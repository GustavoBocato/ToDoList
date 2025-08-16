using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ITodoService _toDoService;
        private readonly IJwtTokenService _jwtTokenService;

        public ClientsController(ITodoService toDoService,
            IJwtTokenService jwtTokenService)
        {
            _toDoService = toDoService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public ActionResult PostClient(ClientDTO clientDTO)
        {
            if (!_toDoService.ValidateClientRegistration(clientDTO)) 
                return BadRequest("O email do cliente a ser registrado já foi cadastrado no sistema.");
            
            var client = _toDoService.CreateClient(clientDTO);
            return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
        }

        [HttpPost("login")]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var client = _toDoService.ValidateLogin(email, password);
                return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
