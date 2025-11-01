using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Weather.Services;
using WeatherApi.Services;

namespace Weather.Config
{
    public static class ServiceExtention
    {
        public static void AddService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IOpenWeatherService,OpenWeatherService>();
            serviceCollection.AddHttpClient<IOpenWeatherService, OpenWeatherService>();
        }
    }
}
