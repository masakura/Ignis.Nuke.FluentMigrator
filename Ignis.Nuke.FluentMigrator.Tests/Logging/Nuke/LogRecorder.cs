using System;
using System.Text;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging.Nuke;

internal sealed class LogRecorder : INukeLogger
{
    private readonly StringBuilder _error = new();
    private readonly string? _prefix;
    private readonly StringBuilder _standard = new();

    public LogRecorder(string? prefix = null)
    {
        _prefix = prefix;
    }

    public void Logger(OutputType type, string message)
    {
        switch (type)
        {
            case OutputType.Std:
                _standard.AppendLine(Message(message));
                break;
            case OutputType.Err:
                _error.AppendLine(Message(message));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private string Message(string message)
    {
        return $"{_prefix}{message}";
    }

    public LogOutput Output()
    {
        return new LogOutput(_standard.ToString(), _error.ToString());
    }
}
