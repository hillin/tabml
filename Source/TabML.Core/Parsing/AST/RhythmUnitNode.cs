using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class RhythmUnitNode : Node, IRequireStringValidation
    {
        public RhythmUnit RhythmUnit { get; }
        public bool NoteValueSpecified { get; }

        public RhythmUnitNode(RhythmUnit rhythmUnit, bool noteValueSpecified = true)
        {
            this.RhythmUnit = rhythmUnit;
            this.NoteValueSpecified = noteValueSpecified;
        }
    }
}
