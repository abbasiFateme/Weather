using System.Text.Json.Serialization;

namespace Weather.Models
{
    public class AirItem
    {
        [JsonPropertyName("main")]
        public AirMain? Main { get; set; }
        [JsonPropertyName("components")]
        public Dictionary<string, double>? Components { get; set; }
        [JsonPropertyName("dt")]
        public long Dt { get; set; }
    }

}

