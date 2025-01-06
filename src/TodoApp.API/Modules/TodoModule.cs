using Carter;

namespace TodoApp.API.Modules;

public class TodoModule(
    TodoHandler todoHandler
) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/todo")
            .WithTags("Todo")
            .WithOpenApi();

        group.MapGet("/weatherforecast", todoHandler.FetchWeatherForecast)
            .WithName("FetchWeatherForecast")
            .WithDescription("Fetch Forecast. This is just a test will remove");

        group.MapGet("/fetch", todoHandler.FetchAllTodos)
            .WithName("FetchTodos")
            .WithDescription("Fetch All Todos");

        group.MapGet("/fetch/{id}", todoHandler.FetchTodoById)
            .WithName("FetchTodoById")
            .WithDescription("Fetch Todo By Id");
        
        group.MapPost("/record", todoHandler.RecordTodo)
            .WithName("RecordTodo")
            .WithDescription("Record Todo");
        
        group.MapPut("/update/{id}", todoHandler.UpdateTodo)
            .WithName("UpdateTodo")
            .WithDescription("Update Todo By ID");
        
        group.MapDelete("/delete/{id}", todoHandler.DeleteTodoById)
            .WithName("DeleteTodoById")
            .WithDescription("Delete Todo By ID");
    }
}