

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Web;
using Weather.Models;
using Weather.Services;

namespace WeatherApi.Services;

public class OpenWeatherService : IOpenWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;

    public OpenWeatherService(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _apiKey = config["OpenWeather:ApiKey"]
            ?? throw new InvalidOperationException("OpenWeather:ApiKey is not configured.");
    }

    public async Task<CityEnvironmentalData?> GetCityEnvironmentalDataAsync(string city,CancellationToken cancellationToken)
    {


        var geocode =await GetGeocodingAsync(city,cancellationToken);
        if (geocode == null || !geocode.Any())
            return null;

        var first = geocode.First();
        double lat = first.Lat;
        double lon = first.Lon;


        var weather = await GetWeatherAsync(lat, lon,cancellationToken);
        var air = await GetAirPollutionAsync(lat, lon, cancellationToken);

        var data = new CityEnvironmentalData
        {
            City = city,
            Latitude = lat,
            Longitude = lon,
            TemperatureCelsius = weather?.Main?.Temp,
            Humidity = weather?.Main?.Humidity,
            WindSpeedMps = weather?.Wind?.Speed,
            AQI = air?.List?.FirstOrDefault()?.Main?.Aqi,
            MajorPollutants = air?.List?.FirstOrDefault()?.Components?.ToDictionary(k => k.Key, v => v.Value)
        };

        return data;
    }


    private async Task<AirPollutionResponse?> GetAirPollutionAsync(double lat, double lon,CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();
        var airUrl = $"https://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={_apiKey}";
        var air = await client.GetFromJsonAsync<AirPollutionResponse>(airUrl, cancellationToken);
        return air;
    }

    private async Task<WeatherResponse?> GetWeatherAsync(double lat, double lon,CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();
        var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        var weather = await client.GetFromJsonAsync<WeatherResponse>(weatherUrl, cancellationToken);
        return weather;
    }

    private async Task<List<GeocodingResponse?>> GetGeocodingAsync(string city,CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();

        var geocodeUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={HttpUtility.UrlEncode(city)}&limit=1&appid={_apiKey}";
        var geocode = await client.GetFromJsonAsync<List<GeocodingResponse>>(geocodeUrl, cancellationToken);
        return geocode;
    }
}


