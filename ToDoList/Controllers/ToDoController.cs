using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoService _toDoService;
        private readonly IJwtTokenService _jwtTokenService;

        public ToDoController(ILogger<ToDoController> logger, IToDoService toDoService,
            IJwtTokenService jwtTokenService)
        {
            _logger = logger;
            _toDoService = toDoService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("registration")]
        public ActionResult postClient(Client client)
        {
            try
            {
                _toDoService.ValidateClientRegistration(client);
                _toDoService.RegisterClient(client);
                return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult login(string email, string password)
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

        [HttpPost("create")]
        public ActionResult postToDoList(ToDoList toDoList)
        {
            try
            {
                var sub = User.FindFirst("sub")?.Value;

                if (!Guid.TryParse(sub, out var clientId))
                    throw new ArgumentException("Id do usuário está mal formado.");

                _toDoService.CheckIfClientExists(clientId);
                var createdToDoList = _toDoService.CreateToDoList(toDoList, clientId);

                return Ok(createdToDoList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
