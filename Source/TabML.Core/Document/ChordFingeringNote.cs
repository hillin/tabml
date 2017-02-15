using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class ChordFingeringNote : Element, IChordFingeringNote
    {
        public const int FingeringSkipString = -1;

        public int Fret { get; set; }
        public LeftHandFingerIndex? FingerIndex { get; set; }
        public bool IsImportant { get; set; }

        public override IEnumerable<Element> Children { get { yield break; } }

        public ChordFingeringNote()
        {

        }

        public ChordFingeringNote(int fret, LeftHandFingerIndex? fingerIndex = null, bool isImportant = false)
        {
            this.Fret = fret;
            this.FingerIndex = fingerIndex;
            this.IsImportant = isImportant;
        }
    }
}
