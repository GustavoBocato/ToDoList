using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models.DTOs
{
    public class ToDoListDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
