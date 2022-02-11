using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildLinuxMessage
{
    [Export(typeof(ISpanClassifier))]
    public class BuildLinuxMessageClassifier : ParserBasedSpanClassifier<BuildLinuxMessageData>
    {
        [ImportingConstructor]
        public BuildLinuxMessageClassifier(IParser<BuildLinuxMessageData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, BuildLinuxMessageData parsedData)
        {
            var messageSpan = parsedData.FullMessage.Span;
            if (parsedData.Type == MessageType.Warning)
            {
                yield return new ProcessedParsedData(messageSpan, ClassificationType.BuildMessageWarning);
            }
            else if (parsedData.Type == MessageType.Error)
            {
                yield return new ProcessedParsedData(messageSpan, ClassificationType.BuildMessageError);
            }
        }
    }
}