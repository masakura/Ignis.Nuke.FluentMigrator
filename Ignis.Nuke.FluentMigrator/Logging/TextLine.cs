using System.Text;
using System.Text.RegularExpressions;

namespace Ignis.Nuke.FluentMigrator.Logging;

internal sealed class TextLine
{
    private readonly StringBuilder _builder = new();
    private readonly string _newLine;

    public TextLine(string newLine)
    {
        _newLine = newLine;
    }

    public void Append(char c)
    {
        _builder.Append(c);
    }

    public bool HasNewLine()
    {
        return _builder.ToString().EndsWith(_newLine);
    }

    public string ReadLine()
    {
        var line = _builder.ToString();
        line = Regex.Replace(line, _newLine + "$", string.Empty, RegexOptions.Multiline);
        _builder.Clear();
        return line;
    }
}
