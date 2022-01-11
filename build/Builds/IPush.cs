using System;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;

namespace Builds;

interface IPush : IPack
{
    private const string NuGetApiKeyEnvironment = "NUGET_API_KEY";

    [Parameter($"NuGet API Key. Default, read from the NUGET_API_KEY environment variable.{NuGetApiKeyEnvironment}")]
    string NugetApiKey => TryGetValue(() => NugetApiKey) ??
                          Environment.GetEnvironmentVariable(NuGetApiKeyEnvironment);

    [Parameter("Push NuGet source. Default is `nuget.org`.")]
    string NugetSource => TryGetValue(() => NugetSource) ?? "nuget.org";

    // ReSharper disable once UnusedMember.Global
    Target Push => _ => _
        .DependsOn(Pack)
        .Executes(() =>
        {
            DotNetTasks.DotNetNuGetPush(s => s
                .SetTargetPath(NuPkgDirectory / "*.nupkg")
                .SetApiKey(NugetApiKey)
                .SetSource(NugetSource));
        });

}
