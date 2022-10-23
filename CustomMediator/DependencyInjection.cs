using System.Reflection;
using CustomMediator.Abstractions.Commands;
using CustomMediator.Abstractions.Queries;
using CustomMediator.Commands;
using CustomMediator.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMediator;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddCommands(assembly);
        services.AddQueries(assembly);
        return services;
    }

    private static void AddCommands(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<ICommandSender, CommandSender>();

        services.Scan(x => x.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
    
    private static void AddQueries(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<IQuerySender, QuerySender>();

        services.Scan(x => x.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}