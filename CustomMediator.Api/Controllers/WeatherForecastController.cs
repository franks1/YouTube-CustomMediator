using CustomMediator.Abstractions.Commands;
using CustomMediator.Api.Commands;
using CustomMediator.Api.Repositories;
using CustomMediator.Api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CustomMediator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly ICommandSender _commandSender;

    public WeatherForecastController(IWeatherForecastRepository weatherForecastRepository, ICommandSender commandSender)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _commandSender = commandSender;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
        => await _weatherForecastRepository.GetAsync(cancellationToken);

    [HttpPost]
    public async Task<ActionResult> Create(CreateForecastRequest request, CancellationToken cancellationToken)
    {
        await _commandSender.SendAsync(new CreateForecastCommand(request.City), cancellationToken);
        return Ok();
    }
    
}