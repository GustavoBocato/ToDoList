using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    [Table("client_todo_list")]
    public class ClientToDoList
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("id_client")]
        public Guid IdClient { get; set; }

        [Column("id_todo_list")]
        public Guid IdToDoList { get; set; }
    }
}
