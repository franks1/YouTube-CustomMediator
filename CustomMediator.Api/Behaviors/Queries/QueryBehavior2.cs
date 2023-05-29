using CustomMediator.Abstractions.Pipeline;
using CustomMediator.Abstractions.Queries;

namespace CustomMediator.Api.Behaviors.Queries;

public class QueryBehavior2<TQuery, TResult> : IQueryPipelineBehavior<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> HandleAsync(TQuery query, QueryHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var result = await next();
        return result;
    }
}