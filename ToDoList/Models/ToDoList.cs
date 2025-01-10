using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class ToDoList
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
    }
}
