using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    abstract class ParserBase<TNode> : IReporter
        where TNode : Node
    {
        public bool HasError { get; private set; }
        public abstract bool TryParse(Scanner scanner, out TNode result);

        protected virtual TNode Recover(Scanner scanner)
        {
            return null;
        }

        protected void Report(ReportLevel level, TextRange? position, string message, params object[] args)
        {
            if (level == ReportLevel.Error)
                this.HasError = true;

            if (level == ReportLevel.Error)
                throw new System.Exception(message);
            //todo
        }

        void IReporter.Report(ReportLevel level, TextRange? position, string message, params object[] args)
        {
            this.Report(level, position, message, args);
        }
    }
}
