using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly WeatherService weatherService;

    public HomeController(WeatherService weatherService)
    {
        this.weatherService = weatherService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Weather(CancellationToken cancellationToken)
    {
        var weatherData = await weatherService.GetWeatherAsync(cancellationToken);
        return Ok(weatherData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult NotFound(string? originalPath)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View(originalPath);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
