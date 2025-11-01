using System.Text.Json.Serialization;

namespace Weather.Models
{
    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

}

