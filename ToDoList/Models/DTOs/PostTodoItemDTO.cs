using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models.DTOs
{
    public class PostTodoItemDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid IdTodolist { get; set; }
    }
}
