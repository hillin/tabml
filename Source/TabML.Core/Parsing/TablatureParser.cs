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
        public override bool TryParse(Scanner scanner, out TablatureNode result)
        {
            result = new TablatureNode();

            while (!scanner.EndOfInput)
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
                    CommandletNode commandlet;
                    if (CommandletParser.Create(scanner).TryParse(scanner, out commandlet))
                        tablature.Nodes.Add(commandlet);
                    break;
                case '|':
                    BarNode bar;
                    if (new BarParser().TryParse(scanner, out bar))
                        tablature.Nodes.Add(bar);
                    break;
            }
        }
    }
}
