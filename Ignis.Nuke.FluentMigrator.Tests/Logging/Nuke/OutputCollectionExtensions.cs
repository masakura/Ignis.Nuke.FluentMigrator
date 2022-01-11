using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common.Tooling;

namespace Ignis.Nuke.FluentMigrator.Logging.Nuke;

internal static class OutputCollectionExtensions
{
    public static string AllText(this IEnumerable<Output> outputs)
    {
        return string.Join(Environment.NewLine, outputs.Select(x => x.Text));
    }
}
