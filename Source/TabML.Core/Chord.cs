using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class Chord
    {
        public NoteName[] Notes { get; }
        public string Name { get; }

        internal Chord(NoteName[] notes, string name)
        {
            if (notes.Length < 2)
                throw new ArgumentException("a chord must be consisted by 2 or more notes", nameof(notes));

            this.Notes = notes;
            this.Name = name;
        }
    }
}
