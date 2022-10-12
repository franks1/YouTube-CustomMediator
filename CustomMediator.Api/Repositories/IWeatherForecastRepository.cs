namespace CustomMediator.Api.Repositories;

public interface IWeatherForecastRepository
{
    Task CreateAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken);
    Task<IEnumerable<WeatherForecast>> GetAsync(CancellationToken cancellationToken);
    Task<WeatherForecast?> GetAsync(string city, CancellationToken cancellationToken);
}