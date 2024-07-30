using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using WeathForecast.DTO;
using WeathForecast.Models;
using System.Configuration;

namespace WeathForecast.ViewModels
{
    public static class WeathForecastVM
    {
        public async static Task<WeatherInfoDto> CityInformationHandler(string city, string resCity)
        {
            return await GetForecast(city, resCity);
        }

        private static string? ReadApiKey(string city, string resCity)
        {
            string json = File.ReadAllText("appsettings.json");

            JObject config = JObject.Parse(json);

            string? apiKey = config["APIKey"]?.ToString();

            return apiKey;
        }

        public static async Task<Forecast?> GetWeather(string query, string? apiKey)
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={query}&appid={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                HttpResponseMessage response;

                try
                {
                    response = await client.GetAsync(apiUrl);
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    throw new TimeoutException("The request timed out.");
                }

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Forecast>(responseBody);
                }
                else
                {
                    throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
                }
            }
        }

        public static async Task<WeatherInfoDto> GetForecast(string city, string resCity)
        {
            var locations = new Forecast();
            string? apiKey = ConfigurationManager.AppSettings["ApiKey"];

            try
            {
                locations = await GetWeather(city, apiKey);
            }
            catch (HttpRequestException)
            {
                try
                {
                    locations = await GetWeather(resCity, apiKey);
                }
                catch (HttpRequestException exResCity)
                {

                    throw new HttpRequestException($"HTTP Error: {exResCity.StatusCode}. Could not get weather for either city.");
                }
            }

            return WeatherInfoDto.TakeInformationDto(locations!);
        }
    }
}
