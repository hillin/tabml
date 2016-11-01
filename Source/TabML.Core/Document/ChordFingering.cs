namespace TabML.Core.Document
{
    public class ChordFingering : Element
    {
        public ChordFingeringNote[] Notes { get; set; }

        public ChordFingering Clone()
        {
            return new ChordFingering
            {
                Notes = (ChordFingeringNote[]) this.Notes?.Clone(),
            };
        }
    }
}
