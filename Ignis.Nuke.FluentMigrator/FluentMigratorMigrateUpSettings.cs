using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Ignis.Nuke.FluentMigrator;

public sealed class FluentMigratorMigrateUpSettings
{
    public List<Action<IMigrationRunnerBuilder>> ConfigureRunners { get; } = new();
    public List<string> Assemblies { get; } = new();

    internal IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb =>
                rb.Services.Configure<AssemblySourceOptions>(options => options.AssemblyNames = Assemblies.ToArray()));

        foreach (var configure in ConfigureRunners) services.ConfigureRunner(configure);
        return services;
    }
}
