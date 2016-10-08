namespace TabML.Parser.Parsing
{
    interface IReporter
    {
        void Report(ReportLevel level, TextRange? position, string message, params object[] args);
    }
}
