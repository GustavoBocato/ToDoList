using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;
        private readonly IJwtTokenService _jwtTokenService;

        public ClientController(ILogger<ClientController> logger, IClientService services,
            IJwtTokenService jwtTokenService)
        {
            _logger = logger;
            _clientService = services;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("registration")]
        public ActionResult postClient(Client client)
        {
            try
            {
                _clientService.validateRegistration(client.email);
                _clientService.registerClient(client);
                return Ok(new {Token = _jwtTokenService.GenerateToken(client)});
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult login(string email, string password)
        {
            try
            {
                var client = _clientService.validateLogin(email, password);
                return Ok(new { Token = _jwtTokenService.GenerateToken(client) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
