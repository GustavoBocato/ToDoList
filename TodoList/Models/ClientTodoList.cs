using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Models
{
    [Table("client_todo_list")]
    public class ClientTodoList
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("id_client")]
        public Guid IdClient { get; set; }

        [Column("id_todo_list")]
        public Guid IdTodoList { get; set; }

        [Column("is_owner")]
        public bool IsOwner { get; set; } = false;
    }
}
