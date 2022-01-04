using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using static Nuke.Common.ValueInjection.ValueInjectionUtility;

namespace Builds;

interface ISolution : INukeBuild
{
    [Solution] Solution Solution => TryGetValue(() => Solution);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    Configuration Configuration => TryGetValue(() => Configuration) ??
                                   (IsLocalBuild ? Configuration.Debug : Configuration.Release);

    AbsolutePath OutputDirectory => RootDirectory / "output";
}
