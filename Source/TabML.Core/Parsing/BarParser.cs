using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    class BarParser : ParserBase<BarNode>
    {

        public override bool TryParse(Scanner scanner, out BarNode result)
        {
            throw new NotImplementedException();
        }

        protected override BarNode Recover()
        {
            return base.Recover();
        }

        protected override void Skip(Scanner scanner)
        {
            base.Skip(scanner);
        }
    }
}
