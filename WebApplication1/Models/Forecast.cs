namespace WebApplication1.Models;

public class Forecast
{
    public int Id { get; set; }
    public DateTime FetchedAt { get; set; }
    public bool IsOnline { get; set; }

    public double? WindDirection { get; set; }
    public double? WindSpeed { get; set; }
    public double? WindGust { get; set; }
    public double? Precipitation { get; set; }
    public double? Temperature { get; set; }
    public double? Humidity { get; set; }
    public double? Pressure { get; set; }
    public double? Light { get; set; }
    public double? DewPoint { get; set; }
    public double? ApparentTemperature { get; set; }

    public string? Sunrise { get; set; }
    public string? Sunset { get; set; }
    public int? MoonPhase { get; set; }
    public bool? IsDay { get; set; }
    public double? TemperatureAvg { get; set; }
    public int? Fog { get; set; }

    public string? RawJson { get; set; }
    public string? ErrorMessage { get; set; }
}