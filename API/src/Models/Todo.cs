using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models;

[Table("Todos")]
public class Todo
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; } = DateTime.Today;
}