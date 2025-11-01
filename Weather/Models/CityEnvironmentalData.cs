namespace Weather.Models
{
    public class CityEnvironmentalData
    {
        public string? City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? TemperatureCelsius { get; set; }
        public int? Humidity { get; set; }
        public double? WindSpeedMps { get; set; }
        public int? AQI { get; set; }
        public Dictionary<string, double>? MajorPollutants { get; set; }
    }

}

