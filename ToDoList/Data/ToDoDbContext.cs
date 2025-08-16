using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        // Define your DbSets (tables)
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Todolist> TodoLists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientTodolist> ClientTodoLists { get; set; }

    }
}
