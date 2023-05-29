namespace CustomMediator.Abstractions.Pipeline;

public interface IQueryPipelineBehavior<in TQuery, TResult> where TQuery : notnull
{
    Task<TResult> HandleAsync(TQuery query, QueryHandlerDelegate<TResult> next, CancellationToken cancellationToken);
}

public delegate Task<TResult> QueryHandlerDelegate<TResult>();