using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class PitchNode : Node
    {
        public NoteNameNode NoteName { get; set; }
        public LiteralNode<int> OctaveGroup { get; set; }
    }
}
