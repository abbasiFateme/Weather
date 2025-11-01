using System.Text.Json.Serialization;

namespace Weather.Models
{
    // Geocoding response (OpenWeatherMap)
    public class GeocodingResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

}

