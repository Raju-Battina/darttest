using System.Net.Http.Json;
using System.Text.Json.Serialization;

public sealed class WeatherService
{
	private readonly HttpClient httpClient;
	private readonly string url;

	public WeatherService(HttpClient httpClient, string url)
	{
		this.httpClient = httpClient;
		this.url = url;
	}

	public async Task<IReadOnlyList<WeatherData>> GetWeatherAsync(CancellationToken cancellationToken = default)
	{
		using var response = await httpClient.GetAsync(url, cancellationToken);
		response.EnsureSuccessStatusCode();

		var weatherResponse = await response.Content.ReadFromJsonAsync<WeatherResponse>(cancellationToken);
		if (weatherResponse?.Data is null)
		{
			throw new InvalidOperationException("The weather response did not contain data.");
		}

		return weatherResponse.Data;
	}

	private sealed class WeatherResponse
	{
		[JsonPropertyName("status")]
		public string? Status { get; set; }

		[JsonPropertyName("data")]
		public List<WeatherData>? Data { get; set; }
	}
}



