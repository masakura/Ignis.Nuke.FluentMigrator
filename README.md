# Ignis.Nuke.FluentMigrator
[Fluent Migrator](https://fluentmigrator.github.io/) [NUKE](https://nuke.build/) Tasks.


## How to use
Add Ignis.Nuke.FluentMigrator to your NUKE build project.

```shell
$ dotnet add build package Ignis.Nuke.FluentMigrator
```

Add MigrateUp NUKE target.

```csharp
Target MigrateUp => _ => _
    .Executes(() =>
    {
        DotNetPublish(s => s
            .SetProjectFile(Solution.GetProject("Your.Migrations")
            .SetConfiguration(Configuration)
            .SetOutput(OutputDirectory / "migrations" / "Your.Migrations.dll"));
        
        FluentMigratorMigrateUp(s => s
            .SetConnectionString("<your database connection string...>")
            .AddAssembly(OutputDirectory / "migrations" / "Your.Migrations.dll")
            .AddConfigureRunner(rb => rb.AddSqlSever2016());
    });
```

```shell
$ dotnet nuke migrate-up
```

## LICENSE
MIT
