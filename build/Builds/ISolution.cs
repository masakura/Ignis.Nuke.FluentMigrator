using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using static Nuke.Common.ValueInjection.ValueInjectionUtility;

namespace Builds;

interface ISolution : INukeBuild
{
    [Solution] Solution Solution => TryGetValue(() => Solution);

    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath CacheDirectory => RootDirectory / ".cache";
}
