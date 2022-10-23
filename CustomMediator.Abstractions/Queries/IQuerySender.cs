namespace CustomMediator.Abstractions.Queries;

public interface IQuerySender
{
    Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}