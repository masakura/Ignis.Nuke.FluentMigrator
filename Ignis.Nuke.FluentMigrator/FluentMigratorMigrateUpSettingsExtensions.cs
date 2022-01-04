using FluentMigrator.Runner;

namespace Ignis.Nuke.FluentMigrator;

public static class FluentMigratorMigrateUpSettingsExtensions
{
    public static FluentMigratorMigrateUpSettings AddConfigureRunner(this FluentMigratorMigrateUpSettings settings,
        Action<IMigrationRunnerBuilder> configure)
    {
        settings.ConfigureRunners.Add(configure);

        return settings;
    }
}
