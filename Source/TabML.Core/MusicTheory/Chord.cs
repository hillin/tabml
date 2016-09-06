using System;

namespace TabML.Core.MusicTheory
{
    public partial class Chord
    {
        public NoteName[] Notes { get; }
        public string Name { get; }

        internal Chord(string name, NoteName[] notes)
        {
            if (notes.Length < 2)
                throw new ArgumentException("a chord must be consisted by 2 or more notes", nameof(notes));

            this.Notes = notes;
            this.Name = name;
        }
    }
}
