using Microsoft.EntityFrameworkCore;
using TodoAPI.API.Models;

namespace TodoAPI.API.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) :base(options) {}

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasKey(t => t.Id);
        }
    }
}
