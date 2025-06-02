using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) { }

        // Define your DbSets (tables)
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientToDoList> ClientToDoLists { get; set; }

    }
}
