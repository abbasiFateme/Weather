using System.Text.Json.Serialization;

namespace Weather.Models
{
    public class WindInfo
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }

}

