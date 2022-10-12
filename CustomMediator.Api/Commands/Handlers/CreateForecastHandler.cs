using CustomMediator.Abstractions.Commands;
using CustomMediator.Api.Repositories;

namespace CustomMediator.Api.Commands.Handlers;

public class CreateForecastHandler : ICommandHandler<CreateForecastCommand>
{
    private readonly IWeatherForecastRepository _repository;

    public CreateForecastHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(CreateForecastCommand command, CancellationToken cancellationToken)
    {
        var forecast = new WeatherForecast
        {
            City = command.City,
            Date = DateTime.Now,
            TemperatureC = 20
        };

        await _repository.CreateAsync(forecast, cancellationToken);
    }
}