using TodoApp.Data.Models;
using System.Text.Json;
using TodoApp.Web.Components.Pages;

namespace TodoApp.Web.Services;

public class TodoService (IHttpClientFactory httpClientFactory)
{
    public async Task<List<WeatherForecast>> FetchWeatherForecast()
    {
        try
        {
            using var client = httpClientFactory.CreateClient("TodoApp.Api");
            var response = await client.GetAsync("/todo/weatherforecast");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<WeatherForecast>>() ?? new List<WeatherForecast>();
            }
            
            throw new HttpRequestException($"Weather forecast request failed with status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            throw new HttpRequestException("Failed to fetch weather forecast", ex);
        }
    }
}