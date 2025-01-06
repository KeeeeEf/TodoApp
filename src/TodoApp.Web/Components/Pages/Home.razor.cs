using Microsoft.AspNetCore.Components;
using TodoApp.Data.Models;
using TodoApp.Web.Services;

namespace TodoApp.Web.Pages;

public partial class Home
{
    [Inject]
    public required TodoService TodoService { get; set; }

     protected List<WeatherForecast> Forecasts { get; set; } = new();

    private string errorMessage { get; set; } = string.Empty;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Forecasts = await TodoService.FetchWeatherForecast() ?? new();
        }
        catch (HttpRequestException ex)
        {
            errorMessage = ex.Message;
            Forecasts = new();
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
}