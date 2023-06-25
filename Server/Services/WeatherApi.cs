using Newtonsoft.Json;
using Server.Helper;

namespace Server.Services;
internal class WeatherApi
{
    private readonly string _apiKey;

    public WeatherApi(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<WeatherResult> GetOneDayWeather(string city)
    {
        string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&&units=metric&appid={_apiKey}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                {
                    dynamic weatherData = JsonConvert.DeserializeObject(responseBody);

                    return new WeatherResult
                    {
                        Success = true,
                        Data = weatherData
                    };
                }

            }
        }

        return new WeatherResult
        {
            Success = false,
            ErrorMessage = "Failed to retrieve weather data."
        };
    }

    public async Task<WeatherResult> GetFiveDayWeather(string city)
    {
        string apiUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&cnt=40&appid={_apiKey}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic weatherData = JsonConvert.DeserializeObject(responseBody);
                var forecastItem = weatherData.list;

                return new WeatherResult
                {
                    Success = true,
                    Data = forecastItem
                };
            }
        }

        return new WeatherResult
        {
            Success = false,
            ErrorMessage = "Failed to retrieve weather data."
        };
    }
}

