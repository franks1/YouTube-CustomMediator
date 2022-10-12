using System.Reflection;
using CustomMediator.Abstractions.Commands;
using CustomMediator.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMediator;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<ICommandSender, CommandSender>();

        services.Scan(x => x.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}