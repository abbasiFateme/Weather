using Microsoft.AspNetCore.Mvc;
using Weather.Services;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IOpenWeatherService _openWeather;

    public WeatherController(IOpenWeatherService openWeather)
    {
        _openWeather = openWeather;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> Get(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest(new { error = "City is required" });

        try
        {
            var result = await _openWeather.GetCityEnvironmentalDataAsync(city);
            if (result == null) 
            return NotFound(new { error = "City not found or no data available" });
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { error = "Bad gateway", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Server error", details = ex.Message });
        }
    }
}