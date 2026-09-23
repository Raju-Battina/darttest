using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

Console.WriteLine("Calling Weather API Client");

//Read configuration from appsettings.json
var configuration = await File.ReadAllTextAsync("appsettings.json");
var appSettings = JsonSerializer.Deserialize<AppSettings>(configuration, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
})
	?? throw new InvalidOperationException("Weather API configuration is empty.");
var settings = appSettings.WeatherApi;

//read dates from dates.txt file
var dates = await File.ReadAllLinesAsync(settings.DatesFile);
Directory.CreateDirectory(settings.JsonFolder);

foreach (var dt in dates.Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => d.Trim()).Distinct() )
{
    //validate date format
    if (!DateOnly.TryParse(dt, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
    {
        Console.Error.WriteLine($"Invalid date format: {dt}. Expected a valid date.");
        continue;
    }

    // Set the date to yyyy-MM-dd format for the API request.
    var dateParm = parsedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    //call the weather API for each date
    var weatherService = new WeatherService(new HttpClient(), settings.Url);

        try
        {
            var responseBody = await weatherService.GetWeatherAsync(dateParm, settings);
            if (JsonNode.Parse(responseBody) is not JsonObject responseJson ||
                responseJson["daily"] is not JsonObject daily ||
                daily["temperature_2m_max"] is not JsonNode temperatureMax ||
                daily["temperature_2m_min"] is not JsonNode temperatureMin)
            {
                throw new JsonException("The response does not contain daily temperature values.");
            }

            var temperatures = new JsonObject
            {
                ["temperature_2m_max"] = temperatureMax.DeepClone(),
                ["temperature_2m_min"] = temperatureMin.DeepClone()
            };

            var responseFile = Path.Combine(settings.JsonFolder, $"{dateParm}.json");
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var temperaturesJson = temperatures.ToJsonString(jsonOptions).Replace("[", "").Replace("]", ""); 
            await File.WriteAllTextAsync(responseFile, temperaturesJson);
            Console.WriteLine(temperaturesJson);
        }
        catch (HttpRequestException exception)
        {
            Console.Error.WriteLine($"Weather request failed: {exception.Message}");
            Environment.ExitCode = 1;
        }
        catch (JsonException exception)
        {
            Console.Error.WriteLine($"Weather response parsing failed: {exception.Message}");
            Environment.ExitCode = 1;
        }
}