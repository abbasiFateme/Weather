using System.Text.Json.Serialization;

namespace Weather.Models
{
    public class AirMain
    {
        [JsonPropertyName("aqi")]
        public int Aqi { get; set; }
    }

}

