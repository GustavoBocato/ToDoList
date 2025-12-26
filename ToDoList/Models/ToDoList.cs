using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    [Table("todo_lists")]
    public class TodoList
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not TodoList other)
                return false;

            return Id == other.Id;
        }   

        public override int GetHashCode() => Id.GetHashCode();
    }
}
