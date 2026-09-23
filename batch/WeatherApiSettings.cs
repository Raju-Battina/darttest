
public sealed class WeatherApiSettings
{
	public string Url { get; set; } = "";
	public string Longitude { get; set; } = "";
	public string Latitude { get; set; } = "";
	public string DatesFile { get; set; } = "";
	public string JsonFolder { get; set; } = "weather-data";
}
