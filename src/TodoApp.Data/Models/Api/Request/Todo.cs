namespace TodoApp.Data.Models.Api.Request;

public record TodoRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}