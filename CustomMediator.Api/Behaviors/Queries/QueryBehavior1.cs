using System.Text.Json;
using CustomMediator.Abstractions.Pipeline;
using CustomMediator.Abstractions.Queries;

namespace CustomMediator.Api.Behaviors.Queries;

public class QueryBehavior1<TQuery, TResult> : IQueryPipelineBehavior<TQuery, TResult>
    where TQuery : IQuery<TResult>
{

    private readonly ILogger<QueryBehavior1<TQuery, TResult>> _logger;

    public QueryBehavior1(ILogger<QueryBehavior1<TQuery, TResult>> logger)
    {
        _logger = logger;
    }

    public async Task<TResult> HandleAsync(TQuery query, QueryHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Query: {type}, {query}",query.GetType(), query);
        var result = await next();
        _logger.LogInformation("Query: {type}, {result}",result?.GetType(), JsonSerializer.Serialize(result));
        return result;
    }
}