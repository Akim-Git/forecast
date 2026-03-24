using System.Text.Json;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class WeatherFetcherService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherFetcherService> _logger;

    public WeatherFetcherService(
        IServiceScopeFactory scopeFactory,
        IConfiguration config,
        IHttpClientFactory httpClientFactory,
        ILogger<WeatherFetcherService> logger)
    {
        _scopeFactory = scopeFactory;
        _config = config;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var intervalSeconds = _config.GetValue<int>("WeatherStation:FetchIntervalSeconds", 3600);

        while (!stoppingToken.IsCancellationRequested)
        {
            await FetchAndStoreAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(intervalSeconds), stoppingToken);
        }
    }

    private async Task FetchAndStoreAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ForecastContext>();

        var url = _config["WeatherStation:Url"];
        var fetchedAt = DateTime.UtcNow;

        var forecast = new Forecast
        {
            FetchedAt = fetchedAt,
            IsOnline = false,
            ErrorMessage = "Meteostanice nebyla dostupná.",
            RawJson = null
        };

        try
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(30);

            var response = await client.GetStringAsync(url, cancellationToken);
            var xml = XDocument.Parse(response);

            var xmlObject = new Dictionary<string, object?>
            {
                [xml.Root!.Name.LocalName] = ConvertElement(xml.Root!)
            };

            forecast.RawJson = JsonSerializer.Serialize(
                xmlObject,
                new JsonSerializerOptions { WriteIndented = true });

            forecast.IsOnline = true;
            forecast.ErrorMessage = null;

            forecast.Temperature = GetSensor(xml, "temperature");
            forecast.Humidity = GetSensor(xml, "humidity");
            forecast.Pressure = GetSensor(xml, "pressure");
            forecast.WindSpeed = GetSensor(xml, "wind_speed");
            forecast.WindDirection = GetSensor(xml, "wind_direction");
            forecast.WindGust = GetSensor(xml, "wind_gust");
            forecast.Precipitation = GetSensor(xml, "precipitation");
            forecast.Light = GetSensor(xml, "exposure");
            forecast.DewPoint = GetSensor(xml, "dew_point");
            forecast.ApparentTemperature = GetSensor(xml, "temperature_apparent");
            forecast.Sunrise = GetVar(xml, "sunrise");
            forecast.Sunset = GetVar(xml, "sunset");
            forecast.IsDay = GetVar(xml, "isday") == "1";
            forecast.MoonPhase = int.TryParse(GetVar(xml, "moonphase"), out var mp) ? mp : null;
            forecast.TemperatureAvg = GetVarDouble(xml, "temperature_avg");
            forecast.Fog = int.TryParse(GetVar(xml, "fog"), out var fog) ? fog : null;
        }
        catch (Exception ex)
        {
            forecast.IsOnline = false;
            forecast.RawJson = null;
            forecast.ErrorMessage = $"Meteostanice nebyla dostupná: {ex.Message}";

            _logger.LogWarning(ex, "Meteostanice nebyla dostupná.");
        }

        db.Forecasts.Add(forecast);
        await db.SaveChangesAsync(cancellationToken);
    }

    private static object? ConvertElement(XElement element)
    {
        var hasAttributes = element.HasAttributes;
        var hasChildElements = element.HasElements;
        var textValue = string.IsNullOrWhiteSpace(element.Value) ? null : element.Value;

        if (!hasAttributes && !hasChildElements)
        {
            return textValue;
        }

        var result = new Dictionary<string, object?>();

        foreach (var attribute in element.Attributes())
        {
            result[$"@{attribute.Name.LocalName}"] = attribute.Value;
        }

        if (hasChildElements)
        {
            var groupedChildren = element.Elements().GroupBy(e => e.Name.LocalName);

            foreach (var group in groupedChildren)
            {
                if (group.Count() == 1)
                {
                    result[group.Key] = ConvertElement(group.First());
                }
                else
                {
                    result[group.Key] = group.Select(ConvertElement).ToList();
                }
            }
        }
        else
        {
            result["#text"] = textValue;
        }

        return result;
    }

    private static double? GetSensor(XDocument xml, string type)
    {
        var sensor = xml.Descendants("sensor")
            .FirstOrDefault(s => s.Element("type")?.Value == type);

        if (sensor == null) return null;

        var value = sensor.Element("value")?.Value;

        if (double.TryParse(
                value,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var result))
        {
            return result;
        }

        return null;
    }

    private static string? GetVar(XDocument xml, string name)
    {
        return xml.Descendants("variable")
            .Elements(name)
            .FirstOrDefault()?.Value;
    }

    private static double? GetVarDouble(XDocument xml, string name)
    {
        var value = GetVar(xml, name);

        if (double.TryParse(
                value,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var result))
        {
            return result;
        }

        return null;
    }
}