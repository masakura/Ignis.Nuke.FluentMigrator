using Nuke.Common;
using Nuke.Common.Tools.GitVersion;

namespace Builds;

interface IGit : INukeBuild
{
    [GitVersion] GitVersion GitVersion => TryGetValue(() => GitVersion);
}
