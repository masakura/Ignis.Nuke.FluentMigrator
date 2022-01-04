using System;
using FluentMigrator;

namespace Ignis.Nuke.FluentMigrator;

[Migration(20220104)]
// ReSharper disable once UnusedType.Global
public sealed class DummyTable : Migration
{
    public override void Up()
    {
        Create.Table("Dummy")
            .WithColumn("id").AsInt32().Identity().PrimaryKey();
    }

    public override void Down()
    {
        throw new NotSupportedException();
    }
}
