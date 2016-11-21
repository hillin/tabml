using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class ChordFingering : Element
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

        public ChordFingering Clone()
        {
            return new ChordFingering
            {
                Notes = (ChordFingeringNote[])this.Notes?.Clone(),
            };
        }
    }
}
