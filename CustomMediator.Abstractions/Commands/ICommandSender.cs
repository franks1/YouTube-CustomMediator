namespace CustomMediator.Abstractions.Commands;

public interface ICommandSender
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand;
}