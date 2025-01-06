using Microsoft.EntityFrameworkCore;
using TodoApp.API.Modules;
using TodoApp.Data.Models;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
builder.Services.AddSingleton<TodoHandler>();

builder.Services.AddDbContextFactory<TodoAppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger UI
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    }); 
}

app.UseHttpsRedirection();

// app.MapPost("/todo", (IDbContextFactory<TodoAppDbContext> dbContextFactory, TodoList TodoListRequest) =>
// {   
//         TodoAppDbContext dbContext = dbContextFactory.CreateDbContext();

//         TodoList entry = new()
//         {
//             Id = id++,
//             Title = "hahaha",
//             Description = "test"
//         };

//         dbContext.TodoList.Add(entry);
//         dbContext.SaveChanges();

//         return Results.Created($"/todo/{entry.Id}", entry);
// })
// .WithName("AddTodo");

app.MapCarter();
app.Run();

public record WeatherForecasts(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
