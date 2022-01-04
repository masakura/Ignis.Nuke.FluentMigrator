using Builds;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild, IClean, ILint
{
    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);
}
