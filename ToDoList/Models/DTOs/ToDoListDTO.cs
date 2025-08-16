using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models.DTOs
{
    public class TodolistDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
