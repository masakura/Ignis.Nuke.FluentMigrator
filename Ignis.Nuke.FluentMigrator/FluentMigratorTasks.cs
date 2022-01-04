using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator;

public static class FluentMigratorTasks
{
    public static IReadOnlyCollection<Output> FluentMigratorMigrateUp(
        Configure<FluentMigratorMigrateUpSettings> configure)
    {
        var settings = configure(new FluentMigratorMigrateUpSettings());
        return FluentMigratorMigrateUp(settings);
    }

    public static IReadOnlyCollection<Output> FluentMigratorMigrateUp(FluentMigratorMigrateUpSettings settings)
    {
        var services = ConfigureServices(settings)
            .BuildServiceProvider();

        services.GetRequiredService<IMigrationRunner>().MigrateUp();

        return new List<Output>().AsReadOnly();
    }

    private static IServiceCollection ConfigureServices(FluentMigratorMigrateUpSettings settings)
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore();

        foreach (var configure in settings.ConfigureRunners) services.ConfigureRunner(configure);
        return services;
    }
}

public sealed class FluentMigratorMigrateUpSettings
{
    public List<Action<IMigrationRunnerBuilder>> ConfigureRunners { get; } = new();
}

public static class FluentMigratorMigrateUpSettingsExtensions
{
    public static FluentMigratorMigrateUpSettings AddConfigureRunner(this FluentMigratorMigrateUpSettings settings,
        Action<IMigrationRunnerBuilder> configure)
    {
        settings.ConfigureRunners.Add(configure);

        return settings;
    }
}
