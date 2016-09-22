using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing.AST
{
    class TimeSignatureCommandletNode : CommandletNode
    {
        public LiteralNode<int> Beats { get; set; }
        public LiteralNode<BaseNoteValue> NoteValue { get; set; }
    }
}
