namespace CustomMediator.Api.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly List<WeatherForecast> Forecasts = new();

    public Task CreateAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken)
    {
        Forecasts.Add(weatherForecast);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAsync(CancellationToken cancellationToken)
        => await Task.FromResult(Forecasts);

    public async Task<WeatherForecast?> GetAsync(string city, CancellationToken cancellationToken)
        => await Task.FromResult(Forecasts.FirstOrDefault(x => x.City == city));
}