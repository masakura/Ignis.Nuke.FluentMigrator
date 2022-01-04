using Nuke.Common.Tools.GitVersion;
using Nuke.Common.ValueInjection;

namespace Builds;

interface IGit
{
    [GitVersion] GitVersion GitVersion => ValueInjectionUtility.TryGetValue(() => GitVersion);
}
