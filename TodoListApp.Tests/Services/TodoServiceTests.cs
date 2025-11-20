using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoListApp.Repositories;
using ToDoListApp.Data;
using ToDoListApp.Mappings;
using ToDoListApp.Models;
using ToDoListApp.Models.DTOs;
using ToDoListApp.Repository;
using ToDoListApp.Services;

namespace TodoListApp.Tests.Services
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockTodoRepository;
        private readonly TodoService _todoService;
        private readonly IMapper _mapper;

        public TodoServiceTests()
        {
            _mockTodoRepository = new Mock<ITodoRepository>();

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

            _mockTodoRepository.Setup<Task<Client>>(r => r.GetClientByEmailAsync(""))
                .ReturnsAsync(new Client());

            var validation = _todoService.ValidateClientRegistration(postClientDTO);

            Assert.Equal(false, validation.Result);
        }
    }
}
