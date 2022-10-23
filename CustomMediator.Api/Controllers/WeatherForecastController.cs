using CustomMediator.Abstractions.Commands;
using CustomMediator.Abstractions.Queries;
using CustomMediator.Api.Commands;
using CustomMediator.Api.Queries;
using CustomMediator.Api.Repositories;
using CustomMediator.Api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CustomMediator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ICommandSender _commandSender;
    private readonly IQuerySender _querySender;

    public WeatherForecastController(ICommandSender commandSender, IQuerySender querySender)
    {
        _commandSender = commandSender;
        _querySender = querySender;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateForecastRequest request, CancellationToken cancellationToken)
    {
        await _commandSender.SendAsync(new CreateForecastCommand(request.City), cancellationToken);
        return Ok();
    }

    [HttpGet("{city}")]
    public async Task<ActionResult> Get([FromRoute] string city, CancellationToken cancellationToken)
    {
        var forecast = await _querySender.SendAsync(new GetForecastByCityQuery(city), cancellationToken);
        return forecast is null ? NotFound() : Ok(forecast);
    }
    
}