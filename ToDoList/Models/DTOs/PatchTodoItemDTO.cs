using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models.DTOs
{
    public class PatchTodoItemDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid? IdTodolist { get; set; }
    }
}
