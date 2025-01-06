namespace TodoApp.Data.Models;

public record WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
    public int TemperatureF { get; set; }
}