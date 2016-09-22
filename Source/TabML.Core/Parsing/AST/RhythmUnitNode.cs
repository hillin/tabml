﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class RhythmUnitNode : Node, IRequireStringValidation
    {
        public NoteValueNode NoteValue { get; set; }
    }
}
