using TabML.Core;
using TabML.Core.Logging;

namespace TabML.Parser.Parsing
{
    public interface IReporter
    {
        void Report(ReportLevel level, TextRange? position, string message, params object[] args);
    }
}
