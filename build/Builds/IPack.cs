using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Builds;

interface IPack : ICompile
{
    AbsolutePath NuPkgDirectory => OutputDirectory / "nupkg";

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var projects = new[]
                {
                    "Ignis.Nuke.FluentMigrator"
                }
                .Select(project => Solution.GetProject(project));
            
            DotNetPack(s => s
                .SetConfiguration(Configuration)
                .SetOutputDirectory(NuPkgDirectory)
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetProperty("EmbedUntrackedSources", true)
                .SetProperty("IncludeSymbols", true)
                .SetProperty("SymbolPackageFormat", "snupkg")
                .SetProperty("PackageLicenseExpression", "MIT")
                .SetAuthors("Tomo Masakura")
                .SetPackageProjectUrl("https://gitlab.com/ignis-build/ignis-nuke-fluentmigrator")
                .SetRepositoryUrl("https://gitlab.com/ignis-build/ignis-nuke-fluentmigrator.git")
                .SetRepositoryType("git")
                .EnableNoRestore()
                .EnableNoBuild()
                .CombineWith(projects, (s, project) => s.SetProject(project)));
        });
}
