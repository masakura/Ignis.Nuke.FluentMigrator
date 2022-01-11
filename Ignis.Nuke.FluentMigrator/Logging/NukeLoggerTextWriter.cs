using System.Text;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class NukeLoggerTextWriter : TextWriter
{
    private readonly StringBuilder _builder = new();
    private readonly Action<string> _logger;

    private NukeLoggerTextWriter(Action<string> logger)
    {
        _logger = logger;
    }

    public override Encoding Encoding { get; } = Encoding.UTF8;

    public override void Write(char value)
    {
        _builder.Append(value);

        if (_builder.ToString().EndsWith(NewLine))
        {
                var message = _builder.ToString();
                message = message[..^NewLine.Length];
                _logger(message);
                _builder.Clear();
        }
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
