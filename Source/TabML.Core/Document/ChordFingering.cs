using System;
using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class ChordFingering : Element, IChordFingering
    {
        public ChordFingeringNote[] Notes { get; set; }
        public override IEnumerable<Element> Children
        {
            get
            {
                if (this.Notes != null)
                    foreach (var note in this.Notes)
                        yield return note;
            }
        }
        
        IReadOnlyList<IChordFingeringNote> IChordFingering.Notes => this.Notes;

        public ChordFingering Clone()
        {
            return new ChordFingering
            {
                Notes = (ChordFingeringNote[])this.Notes?.Clone(),
            };
        }
    }
}
