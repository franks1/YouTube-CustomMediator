namespace CustomMediator.Abstractions.Pipeline;

public interface ICommandPipelineBehavior<in TCommand> where TCommand : notnull
{
    Task HandleAsync(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken);
}

public delegate Task CommandHandlerDelegate();