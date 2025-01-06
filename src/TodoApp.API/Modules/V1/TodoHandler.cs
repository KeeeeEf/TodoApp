using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Models;
using TodoApp.Data.Models.Api.Request;
using TodoApp.Data.Models.Common;

namespace TodoApp.API.Modules;

public class TodoHandler(
    IDbContextFactory<TodoAppDbContext> dbContextFactory
)
{
    public async Task<IEnumerable<WeatherForecasts>> FetchWeatherForecast()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecasts
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
        return forecast;
    }

    public async Task<IResult> FetchAllTodos()
    {
        using var dbContext = dbContextFactory.CreateDbContext();
         
        var todos = await dbContext.TodoList.ToListAsync();

        return Results.Ok(todos);
    }

    public async Task<IResult> FetchTodoById(int id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var todo = await dbContext.TodoList.FindAsync(id);
        
        if (todo is null)
        {
            return Results.NotFound($"Todo with ID {id} not found");
        }

        return Results.Ok(todo);
    }

    public async Task<IResult> RecordTodo(TodoRequest todoRequest)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        TodoList entry = new TodoList
        {
            Title = todoRequest.Title,
            Description = todoRequest.Description
        };
        dbContext.TodoList.Add(entry);
        await dbContext.SaveChangesAsync();
        return Results.Created($"/todo/{entry.Id}", entry);
    }

    public async Task<IResult> UpdateTodo(int id, TodoRequest updatedTodo)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var exisitingTodo = await dbContext.TodoList.FindAsync(id);

        if(exisitingTodo  is null)
        {
            return Results.NotFound($"Todo with ID {id} not found.");
        }

        exisitingTodo.Title = updatedTodo.Title ?? exisitingTodo.Title;
        exisitingTodo.Description = updatedTodo.Description ?? exisitingTodo.Description;

        dbContext.TodoList.Update(exisitingTodo);
        await dbContext.SaveChangesAsync();

        return Results.Ok(exisitingTodo);
    }

    public async Task<IResult> DeleteTodoById(int id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var todo = await dbContext.TodoList.FindAsync(id);

        if(todo is null)
        {
            return Results.NotFound($"Todo with ID {id} not found");
        }

        dbContext.TodoList.Remove(todo);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}