using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Weather.Models;
using WeatherApi.Services;
using Xunit;

namespace Weather.Tests
{
    public class OpenWeatherServiceTests
    {
        private static HttpClient CreateMockHttpClient(Dictionary<string, object> fakeResponses)
        {
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken _) =>
                {
                    var url = request.RequestUri!.ToString();
                    var matched = fakeResponses.FirstOrDefault(f => url.Contains(f.Key));

                    if (matched.Key == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"URL not mocked: {url}")
                        };
                    }

                    var json = JsonSerializer.Serialize(matched.Value);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };
                });

            return new HttpClient(handler.Object);
        }

        [Fact]
        public async Task GetCityEnvironmentalDataAsync_Returns_Expected_Data()
        {
            // Arrange
            var fakeGeo = new List<GeocodingResponse>
            {
                new GeocodingResponse { Name = "Tehran", Lat = 35.6892, Lon = 51.3890 }
            };

            var fakeWeather = new WeatherResponse
            {
                Main = new MainInfo { Temp = 20.5, Humidity = 45 },
                Wind = new WindInfo { Speed = 5.2 }
            };

            var fakeAir = new AirPollutionResponse
            {
                List = new List<AirItem>
                {
                    new AirItem
                    {
                        Main = new AirMain { Aqi = 2 },
                        Components = new Dictionary<string, double>
                        {
                            ["pm2_5"] = 12.3,
                            ["co"] = 0.8
                        }
                    }
                }
            };

            var fakeResponses = new Dictionary<string, object>
            {
                { "geo/1.0/direct", fakeGeo },
                { "data/2.5/weather", fakeWeather },
                { "data/2.5/air_pollution", fakeAir }
            };

            var httpClient = CreateMockHttpClient(fakeResponses);

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);
            CancellationToken cancellationToken = new CancellationToken();  
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["OpenWeather:ApiKey"]).Returns("fake-api-key");

            var service = new OpenWeatherService(factoryMock.Object, configMock.Object);

            // Act
            var result = await service.GetCityEnvironmentalDataAsync("Tehran", cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tehran", result!.City);
            Assert.Equal(35.6892, result.Latitude);
            Assert.Equal(51.3890, result.Longitude);
            Assert.Equal(20.5, result.TemperatureCelsius);
            Assert.Equal(45, result.Humidity);
            Assert.Equal(5.2, result.WindSpeedMps);
            Assert.Equal(2, result.AQI);
            Assert.True(result.MajorPollutants!.ContainsKey("pm2_5"));
        }

        [Fact]
        public async Task GetCityEnvironmentalDataAsync_Returns_Null_For_UnknownCity()
        {
            // Arrange
            var fakeResponses = new Dictionary<string, object>
            {
                { "geo/1.0/direct", new List<GeocodingResponse>() } // empty list
            };

            var httpClient = CreateMockHttpClient(fakeResponses);

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);
            CancellationToken cancellationToken = new CancellationToken();
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["OpenWeather:ApiKey"]).Returns("fake-api-key");

            var service = new OpenWeatherService(factoryMock.Object, configMock.Object);

            // Act
            var result = await service.GetCityEnvironmentalDataAsync("UnknownCity", cancellationToken   );

            // Assert
            Assert.Null(result);
        }
    }
}
