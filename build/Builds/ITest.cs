using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Builds;

interface ITest : ICompile
{

    private AbsolutePath JUnitFile => OutputDirectory / "reports" / "junit"  / "{assembly}-test-result.xml"; 
    
    // ReSharper disable once UnusedMember.Global
    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .AddLoggers($"junit;LogFilePath={JUnitFile};MethodFormat=Class;FailureBodyFormat=Verbose")
                .EnableNoBuild()
                .EnableNoRestore());
        });
}
