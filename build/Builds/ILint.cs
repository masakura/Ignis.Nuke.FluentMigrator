using Ignis.ReSharper.Reporter.InspectCode.Convert.CodeQuality;
using Ignis.ReSharper.Reporter.InspectCode.Convert.Summary;
using Ignis.ReSharper.Reporter.InspectCode.Validations.SeverityLevel;
using Ignis.ReSharper.Reporter.Nuke;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.ReSharper;
using static Nuke.Common.Tools.ReSharper.ReSharperTasks;

namespace Builds;

interface ILint : ICompile
{
    private AbsolutePath InspectCodeFile => OutputDirectory / "reports" / "inspect-code.xml";
    private AbsolutePath CodeQualityFile => OutputDirectory / "reports" / "code-quality.json";

    Target Lint => _ => _
        .Executes(() =>
        {
            ReSharperInspectCode(s => s
                .SetTargetPath(Solution)
                .SetOutput(InspectCodeFile)
                .SetCachesHome(CacheDirectory / "inspect-code")
                .AddProperty("Configuration", Configuration)
                .SetProcessArgumentConfigurator(args => args.Add("--no-build")));
        });

    // ReSharper disable once UnusedMember.Global
    Target CodeQuality => _ => _
        .TriggeredBy(Lint)
        .Executes(() =>
        {
            ReSharperReporterTasks.ReSharperReport(s => s
                .SetInput(InspectCodeFile)
                .SetSeverity(EnsureSeverityLevel.All)
                .AddExport<CodeQualityConverter>(CodeQualityFile)
                .AddExport<SummaryConverter>());
        });
}
