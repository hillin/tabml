using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing.AST
{
    class NoteNameNode : Node
    {
        public LiteralNode<BaseNoteName> BaseNoteName { get; set; }
        public LiteralNode<Accidental> Accidental { get; set; }
    }
}
