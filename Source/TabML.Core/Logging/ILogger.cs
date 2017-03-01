using TabML.Core.Parsing;

namespace TabML.Core.Logging
{
    public interface ILogger
    {
        void Report(LogLevel level, TextRange? position, string message, params object[] args);
    }
}
