using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;

namespace Builds;

interface ISolution : INukeBuild
{
    [Solution] Solution Solution => TryGetValue(() => Solution);

    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath CacheDirectory => RootDirectory / ".cache";
}
