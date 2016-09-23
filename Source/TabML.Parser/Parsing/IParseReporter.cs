namespace TabML.Parser.Parsing
{
    interface IParseReporter
    {
        void Report(ParserReportLevel level, TextRange position, string message, params object[] args);
    }
}
