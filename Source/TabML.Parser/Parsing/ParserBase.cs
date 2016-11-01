using TabML.Core;
using TabML.Core.Logging;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    abstract class ParserBase<TNode> : ILogger
        where TNode : Node
    {
        public bool HasError { get; private set; }
        public abstract bool TryParse(Scanner scanner, out TNode result);

        protected virtual TNode Recover(Scanner scanner)
        {
            return null;
        }

        protected void Report(LogLevel level, TextRange? position, string message, params object[] args)
        {
            if (level == LogLevel.Error)
                this.HasError = true;

            if (level == LogLevel.Error)
                throw new System.Exception(message);
            //todo
        }

        void ILogger.Report(LogLevel level, TextRange? position, string message, params object[] args)
        {
            this.Report(level, position, message, args);
        }
    }
}
