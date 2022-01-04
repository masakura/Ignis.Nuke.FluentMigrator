using Nuke.Common;
using static Nuke.Common.IO.FileSystemTasks;

namespace Builds;

interface IClean : ISolution
{
    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(OutputDirectory);
        });
}
