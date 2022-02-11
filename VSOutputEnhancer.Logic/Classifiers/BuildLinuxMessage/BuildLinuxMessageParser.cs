using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildLinuxMessage
{
    [Export(typeof(IParser<BuildLinuxMessageData>))]
    public class BuildLinuxMessageParser : IParser<BuildLinuxMessageData>
    {
        public Boolean TryParse(SnapshotSpan span, out BuildLinuxMessageData result)
        {
            result = null;
            var text = span.GetText();

            var lowerText = text.ToLowerInvariant();
            // TODO: Should load possible enum values by reflection
            if (!lowerText.Contains(": warning")
                && !lowerText.Contains(": error")
                && !lowerText.Contains(": fatal error"))
            {
                return false;
            }

            var locationVariants = new[]
            {
                "\\d+:\\d+:\\d+:\\d+",
                "\\d+:\\d+",
                "\\d+"
            };
            var regex = $"(?<FilePath>.*?):(?<Location>\\d+:\\d+)?: (?<FullMessage>(?<Type>(fatal |Fatal )?(warning|error|Warning|Error)): (?<Message>.*))\r?\n";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<BuildLinuxMessageData>(match, span.Span);
            return true;
        }
    }
}