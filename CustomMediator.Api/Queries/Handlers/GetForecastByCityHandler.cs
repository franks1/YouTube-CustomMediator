using CustomMediator.Abstractions.Queries;
using CustomMediator.Api.Repositories;

namespace CustomMediator.Api.Queries.Handlers;

public class GetForecastByCityHandler : IQueryHandler<GetForecastByCityQuery, WeatherForecast?>
{
    private readonly IWeatherForecastRepository _repository;

    public GetForecastByCityHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<WeatherForecast?> HandleAsync(GetForecastByCityQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(query.City, cancellationToken);
}