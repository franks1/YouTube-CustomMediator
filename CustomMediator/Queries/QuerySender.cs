using CustomMediator.Abstractions.Pipeline;
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
        
        var behaviors = scope.ServiceProvider.GetServices<IQueryPipelineBehavior<IQuery<TResult>,TResult>>().ToList();

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var methodType =  handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
        
        if (!behaviors.Any())
        {
            return await (Task<TResult>)methodType?.Invoke(handler, new object[] { query, cancellationToken })!;
        }
        
        QueryHandlerDelegate<TResult> next = () =>  (Task<TResult>)methodType?.Invoke(handler, new object[] { query, cancellationToken })!;
        
        behaviors.Reverse();

        foreach (var behavior in behaviors)
        {
            var currentNext = next;
            next = () => behavior.HandleAsync(query, currentNext, cancellationToken);
        }
           

        return await next();
    }
}