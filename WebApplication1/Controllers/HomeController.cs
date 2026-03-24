using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ForecastContext _db;

        public HomeController(ForecastContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var forecasts = await _db.Forecasts
                .OrderByDescending(f => f.FetchedAt)
                .Take(50)
                .ToListAsync();

            return View(forecasts);
        }

        public async Task<IActionResult> Latest()
        {
            var forecasts = await _db.Forecasts
                .OrderByDescending(f => f.FetchedAt)
                .Take(50)
                .Select(f => new
                {
                    fetchedAt = f.FetchedAt.ToString("yyyy-MM-dd HH:mm"),
                    isOnline = f.IsOnline,
                    temperature = f.Temperature,
                    temperatureAvg = f.TemperatureAvg,
                    apparentTemperature = f.ApparentTemperature,
                    humidity = f.Humidity,
                    pressure = f.Pressure,
                    windSpeed = f.WindSpeed,
                    windGust = f.WindGust,
                    windDirection = f.WindDirection,
                    precipitation = f.Precipitation,
                    dewPoint = f.DewPoint,
                    light = f.Light,
                    fog = f.Fog,
                    sunrise = f.Sunrise,
                    sunset = f.Sunset,
                    moonPhase = f.MoonPhase,
                    isDay = f.IsDay
                })
                .ToListAsync();

            return Json(forecasts);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}