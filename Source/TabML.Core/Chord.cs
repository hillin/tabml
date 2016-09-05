using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public sealed partial class Chord
    {
        public BaseNoteName[] Notes { get; }
        public string Name { get; }

        internal Chord(BaseNoteName[] notes, string name)
        {
            if (notes.Length < 2)
                throw new ArgumentException("a chord must be consisted by 2 or more notes", nameof(notes));

            this.Notes = notes;
            this.Name = name;
        }
    }
}
