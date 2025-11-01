

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Web;
using Weather.Models;
using Weather.Services;

namespace WeatherApi.Services;

public class OpenWeatherService : IOpenWeatherService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly string _apiKey;

    public OpenWeatherService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
        _apiKey = config["OpenWeather:ApiKey"]
           ?? throw new InvalidOperationException("OpenWeather:ApiKey is not configured. Set it in appsettings.json or environment variables.");
    }

    public async Task<CityEnvironmentalData?> GetCityEnvironmentalDataAsync(string city)
    {


        var geocode =await GetGeocodingAsync(city);
        if (geocode == null || !geocode.Any())
            return null;

        var first = geocode.First();
        double lat = first.Lat;
        double lon = first.Lon;


        var weather = await GetWeatherAsync(lat, lon);
        var air = await GetAirPollutionAsync(lat, lon);

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


    private async Task<AirPollutionResponse?> GetAirPollutionAsync(double lat, double lon)
    {      
        var airUrl = $"https://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={_apiKey}";
        var air = await _http.GetFromJsonAsync<AirPollutionResponse>(airUrl);
        return air;
    }

    private async Task<WeatherResponse?> GetWeatherAsync(double lat, double lon)
    {    
        var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={_apiKey}";
        var weather = await _http.GetFromJsonAsync<WeatherResponse>(weatherUrl);
        return weather;
    }

    private async Task<List<GeocodingResponse?>> GetGeocodingAsync(string city)
    {
       
        var geocodeUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={HttpUtility.UrlEncode(city)}&limit=1&appid={_apiKey}";
        var geocode = await _http.GetFromJsonAsync<List<GeocodingResponse>>(geocodeUrl);
        return geocode;
    }
}


