using System;
using System.Text;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class LogRecorder : INukeLogger
{
    private readonly StringBuilder _error = new();
    private readonly StringBuilder _standard = new();

    public void Logger(OutputType type, string message)
    {
        switch (type)
        {
            case OutputType.Std:
                _standard.AppendLine(message);
                break;
            case OutputType.Err:
                _error.AppendLine(message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public LogOutput Output()
    {
        return new LogOutput(_standard.ToString(), _error.ToString());
    }
}
