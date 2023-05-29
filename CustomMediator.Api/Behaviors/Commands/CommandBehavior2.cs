using CustomMediator.Abstractions.Commands;
using CustomMediator.Abstractions.Pipeline;

namespace CustomMediator.Api.Behaviors.Commands;

public class CommandBehavior2<TCommand> : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    private readonly ILogger<CommandBehavior2<TCommand>> _logger;

    public CommandBehavior2(ILogger<CommandBehavior2<TCommand>> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("The command is {command}", command);
        await next();
    }
}