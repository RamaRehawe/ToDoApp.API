using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext (DbContextOptions options) : base(options) { }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
