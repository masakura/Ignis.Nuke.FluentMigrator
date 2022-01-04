using Nuke.Common;
using static Nuke.Common.IO.FileSystemTasks;

namespace Builds;

interface IClean : ISolution
{
    // ReSharper disable once UnusedMember.Global
    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(OutputDirectory);
        });
}
