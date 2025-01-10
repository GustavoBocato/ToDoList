using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [Required]
        public int idToDoList { get; set; }
    }
}
