using AutoMapper;
using Moq;
using TodoListApp.Repositories;
using TodoListApp.Services;
using ToDoListApp.Mappings;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Services;

namespace TodoListApp.Tests.Services
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockTodoRepository = new Mock<ITodoRepository>();
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodoServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });

            _mapper = config.CreateMapper();
            _todoService = new TodoService(_mockTodoRepository.Object, _mapper);
        }

        [Fact]
        public async Task ValidateClientRegistration_ShouldReturnFalse_WhenEmailAlreadyExists()
        {
            PostClientDTO postClientDTO = new PostClientDTO()
            {
                Email = ""
            };

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(""))
                .ReturnsAsync(new Client());

            var validation = await _todoService.ValidateClientRegistration(postClientDTO);

            Assert.False(validation);
        }

        [Fact]
        public async Task ValidateClientRegistration_ShouldReturnTrue_WhenEmailDoesNotExist()
        {
            PostClientDTO postClientDTO = new PostClientDTO()
            {
                Email = ""
            };

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(""))
                .ReturnsAsync((Client?) null);

            var validation = await _todoService.ValidateClientRegistration(postClientDTO);

            Assert.True(validation);
        }

        [Fact]
        public async Task ClientEmailAlreadyTaken_ShouldReturnTrue_WhenEmailAlreadyTaken()
        {
            var emailAlreadyTaken = "";

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Client());

            var validation = await _todoService.ClientEmailAlreadyTaken(emailAlreadyTaken);

            Assert.True(validation);
        }

        [Fact]
        public async Task ClientEmailAlreadyTaken_ShouldReturnFalse_WhenEmailWasNotTaken()
        {
            var emailNotYetTaken = "";

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Client) null);

            var validation = await _todoService.ClientEmailAlreadyTaken(emailNotYetTaken);

            Assert.False(validation);
        }

        [Fact]
        public async Task ValidateLogin_ShouldThrowException_WhenEmailDoesNotExist()
        {
            var emailThatDoesNotExistInDB = "";
            var password = "";

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Client) null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _todoService.ValidateLogin(emailThatDoesNotExistInDB, password)
            );
        }

        [Fact]
        public async Task ValidateLogin_ShouldThrowException_WhenThePasswordIsIncorrect()
        {
            var password = "abc";
            var client = new Client()
            {
                Password = "123"
            };

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(client);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _todoService.ValidateLogin("", password)
            );
        }

        [Fact]
        public async Task ValidateLogin_ShouldReturnTheClient_WhenEmailExistsAndPasswordMatches()
        {
            var email = "email";
            var password = "password";
            var client = new Client()
            {
                Email = email,
                Password = password
            };

            _mockTodoRepository.Setup(r => r.GetClientByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(client);

            var result = await _todoService.ValidateLogin("", password);

            Assert.Equal(client, result);
            Assert.IsType<Client>(result);
        }

        [Fact]
        public async Task CreateToDoList_ShouldReturnTodoList()
        {
            var name = "name";
            var description = "description";
            var clientId = new Guid();
            var todoListDTO = new PostTodoListDTO()
            {
                Name = name,
                Description = description
            };

            var todoList = new TodoList()
            {
                Name = name,
                Description = description
            };

            _mockTodoRepository.Setup(r => r.CreateToDoListAsync(It.IsAny<TodoList>(), It.IsAny<Guid>()))
                .ReturnsAsync(todoList);

            var result = await _todoService.CreateToDoList(todoListDTO, clientId);

            Assert.Equal(todoList, result);
            Assert.IsType<TodoList>(result);
        }

        [Fact]
        public async Task GetTodoListsByClientId_ShouldReturnListOfTodoLists()
        {
            var name = "name";
            var description = "description";
            var clientId = new Guid();

            var todoList = new TodoList()
            {
                Name = name,
                Description = description
            };

            List<TodoList> todoLists = [todoList];

            _mockTodoRepository.Setup(r => r.GetTodoListsByClientIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(todoLists);

            var result = await _todoService.GetTodoListsByClientId(clientId);

            Assert.Equal(todoLists, result);
            Assert.IsType<List<TodoList>>(result);
        }

        [Fact]
        public async Task GetTodoItemsByTodoListId_ShouldReturnListOfTodoLists()
        {
            var name = "name";
            var description = "description";
            var todoListId = new Guid();

            var todoItem = new TodoItem()
            {
                Name = name,
                Description = description,
                IdTodolist = todoListId
            };

            List<TodoItem> todoItems = [todoItem];

            _mockTodoRepository.Setup(r => r.GetTodoItemsByTodoListIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(todoItems);

            var result = await _todoService.GetTodoItemsByTodoListId(todoListId);

            Assert.Equal(todoItems, result);
            Assert.IsType<List<TodoItem>>(result);
        }
    }
}
