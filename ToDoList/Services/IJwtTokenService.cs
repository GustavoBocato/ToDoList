using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IJwtTokenService
    {
        public string GenerateToken(Client client);
    }
}
