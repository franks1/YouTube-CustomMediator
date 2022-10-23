using CustomMediator.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMediator.Queries;

public class QuerySender : IQuerySender
{
    private readonly IServiceProvider _serviceProvider;

    public QuerySender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        return await (Task<TResult>) handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))?
            .Invoke(handler, new object[]{query, cancellationToken})!;
    }
}