using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public struct TimeSignature
    {
        public int Beats { get; }
        public BaseNoteValue NoteValue { get; }

        public TimeSignature(int beats, BaseNoteValue noteValue)
        {
            Beats = beats;
            NoteValue = noteValue;
        }
    }
}
