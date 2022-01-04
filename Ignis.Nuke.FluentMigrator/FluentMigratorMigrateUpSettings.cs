using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Ignis.Nuke.FluentMigrator;

public sealed class FluentMigratorMigrateUpSettings
{
    public List<Action<IMigrationRunnerBuilder>> ConfigureRunners { get; } = new();

    internal IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore();

        foreach (var configure in ConfigureRunners) services.ConfigureRunner(configure);
        return services;
    }
}
