using TodoList.Data;
using TodoList.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Extensions;

public static class Extensions
{
    public static IServiceCollection AddTodoServices(this IServiceCollection services)
        => services
            .AddScoped<ITodoRepository, TodoRepository>();

    public static IServiceScope AddMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var todoDBContext = scope.ServiceProvider.GetRequiredService<TodoDBContext>();
        todoDBContext.Database.Migrate();
        return scope;
    }
}