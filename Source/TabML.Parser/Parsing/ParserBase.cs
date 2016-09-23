using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    abstract class ParserBase<TNode> : IParseReporter
        where TNode : Node
    {
        public bool HasError { get; private set; }
        public abstract bool TryParse(Scanner scanner, out TNode result);

        protected virtual TNode Recover(Scanner scanner)
        {
            return null;
        }
        
        protected void Report(ParserReportLevel level, TextRange position, string message, params object[] args)
        {
            if (level == ParserReportLevel.Error)
                this.HasError = true;

            //todo
        }

        void IParseReporter.Report(ParserReportLevel level, TextRange position, string message, params object[] args)
        {
            this.Report(level, position, message, args);
        }
    }
}
