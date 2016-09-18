using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class ChordCommandletNode : CommandletNode, IRequireStringValidation
    {
        public ChordDefinition ChordDefinition { get; }

        public ChordCommandletNode(ChordDefinition chordDefinition)
        {
            this.ChordDefinition = chordDefinition;
        }

    }
}
