using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    class TablatureParser : ParserBase<TablatureNode>
    {
        public override bool IsRecoverable => false;

        public override bool TryParse(Scanner scanner, out TablatureNode result)
        {
            result = new TablatureNode();

            while (!scanner.EndOfFile)
            {
                this.ParseNode(scanner, result);
            }

            return true;
        }

        private void ParseNode(Scanner scanner, TablatureNode tablature)
        {
            scanner.SkipWhitespaces();
            switch (scanner.Peek())
            {
                case '+':
                    tablature.Nodes.Add(new CommandletParser().TryParse(scanner));
                    break;
                case '|':
                    tablature.Nodes.Add(new BarParser().TryParse(scanner));
                    break;
            }
        }
    }
}
