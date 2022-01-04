using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Ignis.Nuke.FluentMigrator;

public static class FluentMigratorMigrateUpSettingsExtensions
{
    public static FluentMigratorMigrateUpSettings AddConfigureRunner(this FluentMigratorMigrateUpSettings settings,
        Action<IMigrationRunnerBuilder> configure)
    {
        settings.ConfigureRunners.Add(configure);

        return settings;
    }

    /// <summary>
    ///     Set the Connection String of the database to be migrated.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static FluentMigratorMigrateUpSettings SetConnectionString(this FluentMigratorMigrateUpSettings settings,
        string connectionString)
    {
        return settings.AddConfigureRunner(rb => rb
            .Services.Configure<ProcessorOptions>(options => options.ConnectionString = connectionString));
    }
}
