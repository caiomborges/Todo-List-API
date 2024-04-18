using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data;

public class TodoDBContext : DbContext
{
    public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options) { }

    public DbSet<Todo> Todo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>();
    }
}