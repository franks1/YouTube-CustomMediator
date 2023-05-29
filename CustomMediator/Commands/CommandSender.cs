using CustomMediator.Abstractions.Commands;
using CustomMediator.Abstractions.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMediator.Commands;

public class CommandSender : ICommandSender
{
    private readonly IServiceProvider _serviceProvider;

    public CommandSender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();

        var behaviors = scope.ServiceProvider.GetServices<ICommandPipelineBehavior<TCommand>>().ToList();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        if (!behaviors.Any())
        {
            await handler.HandleAsync(command, cancellationToken);
            return;
        }

        CommandHandlerDelegate next = () => handler.HandleAsync(command, cancellationToken);

        behaviors.Reverse();

        foreach (var behavior in behaviors)
        {
            var currentNext = next;
            next = () => behavior.HandleAsync(command, currentNext, cancellationToken);
        }

        await next();
    }
}