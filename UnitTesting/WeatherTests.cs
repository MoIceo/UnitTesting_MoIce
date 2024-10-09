using Moq;
using TestingLib.Weather;

namespace UnitTesting
{
    public class WeatherTests
    {
        private readonly Mock<IWeatherForecastSource>? mockWeatherForecastSource;

        public WeatherTests()
        {
            mockWeatherForecastSource = new Mock<IWeatherForecastSource>();
        }

        [Fact]
        public void GetWeatherForecast_ShouldReturnForecast()
        {
            var weatherForecast = new WeatherForecast { Date = DateTime.Today, TemperatureC = 666, Summary = "Всем [ОТРЕДАКТИРОВАННО]!" };

            mockWeatherForecastSource?.Setup(repo => repo.GetForecast(DateTime.Today)).Returns(weatherForecast);

            var service = new WeatherForecastService(mockWeatherForecastSource.Object);

            var result = service.GetWeatherForecast(DateTime.Today);

            Assert.NotNull(result);
        }
        
    }
}
