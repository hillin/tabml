using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    class AlternateCommandletNode : CommandletNode
    {
        public LiteralNode<string> AlternationText { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get { yield return this.AlternationText; }
        }
    }
}
