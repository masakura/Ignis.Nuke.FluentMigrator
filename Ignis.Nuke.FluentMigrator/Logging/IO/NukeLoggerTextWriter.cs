using System.Text;
using Ignis.Nuke.FluentMigrator.Text;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging.IO;

internal sealed class NukeLoggerTextWriter : TextWriter
{
    private readonly TextLine _line;
    private readonly Action<string> _logger;

    private NukeLoggerTextWriter(Action<string> logger)
    {
        _logger = logger;
        _line = new TextLine(NewLine);
    }

    public override Encoding Encoding { get; } = Encoding.UTF8;

    public override void Write(char value)
    {
        _line.Append(value);

        if (!_line.HasNewLine()) return;

        _logger(_line.ReadLine());
    }

    public override void Flush()
    {
    }

    public static TextWriter Std(Action<OutputType, string> logger)
    {
        return new NukeLoggerTextWriter(message => logger(OutputType.Std, message));
    }

    public static TextWriter Err(Action<OutputType, string> logger)
    {
        return new NukeLoggerTextWriter(message => logger(OutputType.Err, message));
    }
}
