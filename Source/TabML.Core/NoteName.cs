using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TabML.Core.NoteNames;

namespace TabML.Core
{
    public struct NoteName
    {
        private static readonly Dictionary<int, NoteName>[] SemitoneToNoteNameLookup
            =
        {
            /*0*/
            new Dictionary<int, NoteName>
            {
                [7] = BSharp,
                [0] = C,
                [1] = new NoteName(BaseNoteName.D, Accidental.DoubleFlat)
            },

            /*1*/
            new Dictionary<int, NoteName>
            {
                [0] = new NoteName(BaseNoteName.D, Accidental.DoubleSharp),
                [1] = new NoteName(BaseNoteName.D, Accidental.DoubleFlat)
            },

        };

        public static NoteName FromSemitones(int semitones, int? degreeToSnap)
        {
            
        }

        public BaseNoteName BaseName { get; }
        public Accidental Accidental { get; }

        public NoteName(BaseNoteName baseName, Accidental accidental)
        {
            this.BaseName = baseName;
            this.Accidental = accidental;
        }

        public int GetSemitones()
        {
            return (this.BaseName.GetSemitones() + this.Accidental.GetSemitoneOffset() + 12) % 12;
        }

    }
}
