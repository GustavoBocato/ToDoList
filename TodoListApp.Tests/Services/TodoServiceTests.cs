using Moq;
using ToDoListApp.Repository;
using ToDoListApp.Services;

namespace TodoListApp.Tests.Services
{
    public class TodoServiceTests
    {
        private readonly Mock<TodoRepository> _mockClientRepository;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _mockClientRepository = new Mock<TodoRepository>();
            _todoService = new TodoService(_mockClientRepository.Object);
        }

        [Fact]
        public async Task ValidateClientRegistration_ShouldReturnFalse_WhenEmailAlreadyExists()
        {
            // Template dos testes
        }
    }
}
