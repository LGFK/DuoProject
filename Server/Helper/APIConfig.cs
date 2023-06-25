using Microsoft.Extensions.Configuration;

namespace Server.Helper;
public static class APIConfig
{
    private static  string? _apiKey;
    private static  string? _apiUrl;

    public static string GetUrl(string? city)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./config/appsetings.json")
            .Build();
        _apiKey = configuration["ApiKey"];
        _apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&&units=metric&appid={_apiKey}";
        return _apiUrl;
    }
}
