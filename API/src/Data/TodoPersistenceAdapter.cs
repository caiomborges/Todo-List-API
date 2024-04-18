using System.Net;
using Microsoft.EntityFrameworkCore;
using TodoList.Interfaces;
using TodoList.Models;

namespace TodoList.Data;

public class TodoPersistenceAdapter(TodoDBContext dbContext) : ITodoRepository
{
    public async Task<List<Todo>?> GetAll()
    {
        return await dbContext.Todo.ToListAsync();
    }

    public async Task<Todo?> Get(Guid id)
    {
        return await dbContext.Todo.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Todo?> Create(Todo todo)
    {
        await dbContext.Todo.AddAsync(todo);
        await dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task<Todo?> Update(Todo todo, Guid id)
    {
        var todoToUpdate = await dbContext.Todo.FirstOrDefaultAsync(x => x.Id == id);

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
        var todoToDelete = await dbContext.Todo.FirstOrDefaultAsync(x => x.Id == id);

        if (todoToDelete!=null)
        {
            dbContext.Todo.Remove(todoToDelete);
            await dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}