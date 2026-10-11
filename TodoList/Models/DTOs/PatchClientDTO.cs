using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models.DTOs
{
    public class PatchClientDTO
    {
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
