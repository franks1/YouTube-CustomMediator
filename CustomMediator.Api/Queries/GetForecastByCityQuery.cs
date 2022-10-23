using CustomMediator.Abstractions.Queries;

namespace CustomMediator.Api.Queries;

public record GetForecastByCityQuery(string City) : IQuery<WeatherForecast?>;
