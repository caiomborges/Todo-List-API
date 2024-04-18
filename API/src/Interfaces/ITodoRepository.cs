using TodoList.Models;

namespace TodoList.Interfaces;

public interface ITodoRepository
{
    Task<List<Todo>?> GetAll();
    Task<Todo?> Get(Guid id);
    Task<Todo?> Create(Todo todo);
    Task<Todo?> Update(Todo todo, Guid id);
    Task<bool> Delete(Guid id);
}