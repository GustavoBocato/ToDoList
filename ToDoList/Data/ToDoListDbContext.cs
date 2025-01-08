using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) { }

        // Define your DbSets (tables)
        public DbSet<ToDoItem> todoitems { get; set; }
        public DbSet<ToDoList> todolists { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<ClientToDoList> clienttodolists { get; set; }

    }
}
