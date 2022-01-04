using System.Reflection;
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
    ///     Add an assembly with migration.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="assembly"></param>
    public static FluentMigratorMigrateUpSettings AddAssembly(this FluentMigratorMigrateUpSettings settings,
        string assembly)
    {
        return settings.AddAssembly(Assembly.LoadFrom(assembly));
    }

    /// <summary>
    ///     Add an assembly with migration.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="assembly"></param>
    // ReSharper disable once MemberCanBePrivate.Global
    public static FluentMigratorMigrateUpSettings AddAssembly(this FluentMigratorMigrateUpSettings settings,
        Assembly assembly)
    {
        return settings.AddAssemblies(assembly);
    }

    /// <summary>
    ///     Add assemblies with migration.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static FluentMigratorMigrateUpSettings AddAssemblies(this FluentMigratorMigrateUpSettings settings,
        params Assembly[] assemblies)
    {
        return settings.AddAssemblies((IEnumerable<Assembly>) assemblies);
    }

    /// <summary>
    ///     Add assemblies with migration.
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static FluentMigratorMigrateUpSettings AddAssemblies(this FluentMigratorMigrateUpSettings settings,
        IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies) settings.Assemblies.Add(assembly);

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
