
using WeathForecast.Mapping;
using WeathForecast.Models;

namespace WeathForecast.DTO
{
    public class WeatherInfoDto
    {
        public string? City { get; set; }

        public TimeSpan? CityCurrentTime { get; set; }

        public TimeSpan? ServerCurrentTime { get; set; }

        public TimeSpan? TimeDifferenceBetweenCityAndServer { get; set; }

        public int? CelsiusTemperature { get; set; }

        public int? AtmosphericPressure { get; set; }

        public int? AirHumidity { get; set; }

        public double? WindSpeed { get; set; }

        public int? CloudCover { get; set; }

        public string? Main {  get; set; }

        internal static WeatherInfoDto TakeInformationDto(Forecast forecast)
        {
            return new WeatherInfoDto()
            {
                City = forecast.Name,
                CityCurrentTime = Time.TakeCityCurrentTime(forecast.Timezone),
                ServerCurrentTime = Time.TakeServerCurrentTime(forecast.Timezone),
                TimeDifferenceBetweenCityAndServer = Time.TakeTimeDifferenceBetweenCityAndServer(forecast.Timezone),
                CelsiusTemperature = forecast.Main != null ? (int?)Math.Round(forecast.Main.Temp - 273.15) : null,
                AtmosphericPressure = forecast.Main?.Pressure,
                AirHumidity = forecast.Main?.Humidity,
                WindSpeed = forecast.Wind != null ? Math.Round(forecast.Wind.Speed, 1) : null,
                CloudCover = forecast.Clouds?.All,
                Main = forecast.Weather?[0].Main
            };
        }
    }
}

