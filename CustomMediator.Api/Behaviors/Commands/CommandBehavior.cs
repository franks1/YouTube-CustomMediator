using CustomMediator.Abstractions.Commands;
using CustomMediator.Abstractions.Pipeline;

namespace CustomMediator.Api.Behaviors.Commands;

public class CommandBehavior<TCommand> : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    private readonly ILogger<CommandBehavior<TCommand>> _logger;

    public CommandBehavior(ILogger<CommandBehavior<TCommand>> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("The command type is {type}", command.GetType());
        await next();
    }
}