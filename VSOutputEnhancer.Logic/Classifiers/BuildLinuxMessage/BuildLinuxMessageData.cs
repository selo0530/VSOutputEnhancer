using System;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildLinuxMessage
{
    // TODO: Review accessibility level
    public class BuildLinuxMessageData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public BuildLinuxMessageData()
        {
        }

        public BuildLinuxMessageData(
            ParsedValue<MessageType> type,
            ParsedValue<String> message,
            ParsedValue<String> fullMessage,
            ParsedValue<String> filePath)
        {
            Type = type;
            Message = message;
            FullMessage = fullMessage;
            FilePath = filePath;
        }

        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<MessageType> Type { get; private set; }
        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<String> FullMessage { get; private set; }
        public ParsedValue<String> FilePath { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}