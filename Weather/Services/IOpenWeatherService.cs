using Weather.Models;

namespace Weather.Services
{
    public interface IOpenWeatherService
    {
        Task<CityEnvironmentalData?> GetCityEnvironmentalDataAsync(string city);
    }

}
