using System.Text.Json.Serialization;

public class WeatherData
{
    [JsonPropertyName("temperature_2m_max")]
    public double Temperature2mMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public double Temperature2mMin { get; set; }

    [JsonPropertyName("precipitation_sum")]
    public double PrecipitationSum { get; set; }

    [JsonPropertyName("dateOn")]
    public DateOnly DateOn { get; set; }
}