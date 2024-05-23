using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult
{
    [Export(typeof(IParser<BuildResultData>))]
    public class BuildResultParser : IParser<BuildResultData>
    {
        public Boolean TryParse(SnapshotSpan span, out BuildResultData result)
        {
            var text = span.GetText();

            result = null;
            if (!text.StartsWith("========== ", StringComparison.Ordinal))
            {
                return false;
            }
            if (!text.EndsWith(" ==========\r\n", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = "^========== (?:Build|Rebuild All|Clean): (?<Succeeded>\\d+) succeeded(?: or up-to-date)?, (?<Failed>\\d+) failed, (?:(?<UpToDate>\\d+) up-to-date, )?(?<Skipped>\\d+) skipped ==========\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
				regex = "^========== (?:빌드|모두 다시 빌드|정리): 성공 (?<Succeeded>\\d+), 실패 (?<Failed>\\d+)(, (?:최신 (?<UpToDate>\\d+), )?(?:생략 (?<Skipped>\\d+)))? ==========\r\n$";
				match = Regex.Match(text, regex, RegexOptions.Compiled);
                if (!match.Success)
				{
                    return false;
                }
            }

            result = ParsedData.Create<BuildResultData>(match, span.Span);
            return true;
        }
    }
}