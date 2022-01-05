using FluentMigrator.Runner.VersionTableInfo;

namespace Ignis.Nuke.FluentMigrator.Migrations;

[VersionTableMetaData]
// ReSharper disable once UnusedType.Global
public sealed class CustomVersionInfo : IVersionTableMetaData
{
#pragma warning disable CS0618
    private readonly IVersionTableMetaData _default = new DefaultVersionTableMetaData();
#pragma warning restore CS0618
    
    public object? ApplicationContext { get; set; } = null;

    public bool OwnsSchema => _default.OwnsSchema;
    public string SchemaName => _default.SchemaName;
    public string TableName => $"Custom{_default.TableName}";
    public string ColumnName => _default.ColumnName;
    public string DescriptionColumnName => _default.DescriptionColumnName;
    public string UniqueIndexName => _default.UniqueIndexName;
    public string AppliedOnColumnName => _default.AppliedOnColumnName;
}
