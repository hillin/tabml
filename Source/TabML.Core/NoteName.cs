using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public struct NoteName
    {
        public static NoteName FromSemitones(int semitones)
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
