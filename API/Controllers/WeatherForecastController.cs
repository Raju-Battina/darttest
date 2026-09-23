using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace darttestApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public WeatherController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet(Name = "v1/weather")]
    public ActionResult<IList<WeatherData>> Get()
    {
      return Ok(new { status = "Success", data = GetWeatherData() });
    }

    private IList<WeatherData> GetWeatherData()
    {
        var weatherDataList = new List<WeatherData>();

        var folderPath = _configuration["WeatherDataFolder"]
            ?? _configuration["Weather-dataFolder"]
            ?? "weather-data";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Folder does not exist.");
            return weatherDataList;
        }

        string[] files = Directory.GetFiles(
            folderPath,
            "*.*",
            SearchOption.AllDirectories
        );

        foreach (string file in files)
        {
            Console.WriteLine($"Reading file: {file}");

            try
            {
                string content = System.IO.File.ReadAllText(file);
                WeatherData? weatherData = JsonSerializer.Deserialize<WeatherData>(
                    content,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    });

                if (weatherData is null)
                {
                    Console.WriteLine($"No weather data found in {file}.");
                    continue;
                }

                weatherData.DateOn = DateOnly.Parse(Path.GetFileNameWithoutExtension(file));
                weatherDataList.Add(weatherData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading {file}: {ex.Message}");
            }
        }

        return weatherDataList;
    }
}
