using Nuke.Common;
using Nuke.Common.Tools.DotNet;

namespace Builds;

interface ITest : ICompile
{
    // ReSharper disable once UnusedMember.Global
    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTasks.DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore());
        });
}
