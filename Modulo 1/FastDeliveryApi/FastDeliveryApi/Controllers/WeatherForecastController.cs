using Microsoft.AspNetCore.Mvc;

namespace FastDeliveryApi.Controllers;

[ApiController]
[Route("Josseline")]//Aqui se cambia nombre
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("ObtenerClima/{Name}")]//Aqui nombre de metodo
    public WeatherForecast Get(string Name)
    {
        if(!string.IsNullOrWhiteSpace(Name))
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).First(s => s.Summary == Name);
        }
        WeatherForecast response = new();
        return response;
    }
}
