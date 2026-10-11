using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models.DTOs
{
    public class PostTodoListDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
