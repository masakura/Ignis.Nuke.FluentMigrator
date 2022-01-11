using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator;

public static class FluentMigratorTasks
{
    public static Action<OutputType, string> Logger { get; set; } = ProcessTasks.DefaultLogger;

    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IReadOnlyCollection<Output> FluentMigratorMigrateUp(
        Configure<FluentMigratorMigrateUpSettings> configure)
    {
        var settings = configure(new FluentMigratorMigrateUpSettings());
        return FluentMigratorMigrateUp(settings);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IReadOnlyCollection<Output> FluentMigratorMigrateUp(FluentMigratorMigrateUpSettings settings)
    {
        var services = settings.ConfigureServices()
            .BuildServiceProvider();

        services.GetRequiredService<IMigrationRunner>().MigrateUp();

        return new List<Output>().AsReadOnly();
    }
}
