using System.Text.Json.Serialization;

namespace Weather.Models
{
    public class AirPollutionResponse
    {
        [JsonPropertyName("list")]
        public List<AirItem>? List { get; set; }
    }

}

