using Microsoft.EntityFrameworkCore;
using TodoList.Interfaces;
using TodoList.Models;

namespace TodoList.Data;

public class TodoRepository(TodoDBContext dbContext) : ITodoRepository
{
    public async Task<List<Todo>?> GetAll()
    {
        var todos = await dbContext.Todos.ToListAsync();
        
        if(todos is null || todos.Count == 0)
        {
            return null;
        }

        return todos;
    }

    public async Task<Todo?> Get(Guid id)
    {
        return await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Todo?> Create(Todo todo)
    {
        await dbContext.Todos.AddAsync(todo);
        await dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task<Todo?> Update(Todo todo, Guid id)
    {
        var todoToUpdate = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (todoToUpdate!=null)
        {
            todoToUpdate.Name = todo.Name;
            todoToUpdate.Description = todo.Description;
            todoToUpdate.Status = todo.Status;
            todoToUpdate.DueDate = todo.DueDate;
            await dbContext.SaveChangesAsync();
        }

        return todoToUpdate;
    }

    public async Task<bool> Delete(Guid id)
    {
        var todoToDelete = await dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (todoToDelete!=null)
        {
            dbContext.Todos.Remove(todoToDelete);
            await dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}