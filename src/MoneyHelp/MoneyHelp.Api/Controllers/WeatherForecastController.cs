using Microsoft.AspNetCore.Mvc;
using MoneyHelp.DataAccess.Abstractions.Models;
using MoneyHelp.DataAccess.Abstractions.Repositories;

namespace MoneyHelp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWalletRepository _walletRepository;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWalletRepository walletRepository, ILogger<WeatherForecastController> logger)
    {
        _walletRepository = walletRepository;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> GetAsync(CancellationToken ct)
    {
        var a = new Wallet
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };

        var s = await _walletRepository.Add(a, ct);

        return Ok(s);
    }
}
