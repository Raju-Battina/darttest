public sealed class WeatherService
{
	private readonly HttpClient httpClient;
	private readonly string url;

	public WeatherService(HttpClient httpClient, string url)
	{
		this.httpClient = httpClient;
		this.url = url;
	}

	public async Task<string> GetWeatherAsync(string date, WeatherApiSettings settings)
	{
		var requestUri = new UriBuilder(url)
		{
			Query = $"latitude={Uri.EscapeDataString(settings.Latitude)}&longitude={Uri.EscapeDataString(settings.Longitude)}&start_date={Uri.EscapeDataString(date)}&end_date={Uri.EscapeDataString(date)}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=auto"
		}.Uri;

		using var response = await httpClient.GetAsync(requestUri);
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadAsStringAsync();
	}
}

public sealed class AppSettings
{
	public WeatherApiSettings WeatherApi { get; set; } = new();
}


