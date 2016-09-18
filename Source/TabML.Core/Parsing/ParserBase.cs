using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    abstract class ParserBase<TNode> : IParseReporter
        where TNode : Node
    {
        public abstract bool TryParse(Scanner scanner, out TNode result);

        protected virtual TNode Recover(Scanner scanner)
        {
            return null;
        }
        
        protected void Report(ParserReportLevel level, TextRange position, string message, params object[] args)
        {
            //todo
        }

        void IParseReporter.Report(ParserReportLevel level, TextRange position, string message, params object[] args)
        {
            this.Report(level, position, message, args);
        }
    }
}
