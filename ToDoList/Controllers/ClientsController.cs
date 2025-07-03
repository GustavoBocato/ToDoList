using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IToDoService _toDoService;
        private readonly IJwtTokenService _jwtTokenService;

        public ClientsController(ILogger<ClientsController> logger, IToDoService toDoService,
            IJwtTokenService jwtTokenService)
        {
            _logger = logger;
            _toDoService = toDoService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public ActionResult PostClient(ClientDTO clientDTO)
        {
            try
            {
                _toDoService.ValidateClientRegistration(clientDTO);
                var client = _toDoService.CreateClient(clientDTO);
                return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
