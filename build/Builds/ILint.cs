using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReSharper;
using static Nuke.Common.Tools.ReSharper.ReSharperTasks;

namespace Builds;

interface ILint : ICompile
{
    private AbsolutePath InspectCodeFile => OutputDirectory / "reports" / "inspect-code.xml";

    // ReSharper disable once UnusedMember.Global
    Target Lint => _ => _
        .Executes(() =>
        {
            ReSharperInspectCode(s => s
                .SetTargetPath(Solution)
                .SetOutput(InspectCodeFile)
                .SetCachesHome(CacheDirectory / "inspect-code")
                .AddProperty("Configuration", Configuration)
                .SetProcessArgumentConfigurator(args => args.Add("--no-build")));
        });
}
