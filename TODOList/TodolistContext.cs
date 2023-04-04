using Microsoft.EntityFrameworkCore;

namespace Todolist
{
    public class TodolistContext : DbContext
    {
        public TodolistContext(DbContextOptions<TodolistContext> options)
            :base(options)
        {
        }

        public DbSet<TodoItem> Items { get; set; }
    }
}
