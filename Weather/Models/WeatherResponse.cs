using System.Text.Json.Serialization;

namespace Weather.Models
{

    public class WeatherResponse
    {
        [JsonPropertyName("main")]
        public MainInfo? Main { get; set; }
        [JsonPropertyName("wind")]
        public WindInfo? Wind { get; set; }
    }

}

