using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core;

namespace TabML.Parser.Document
{
    public class ChordFingeringNote : Element
    {
        public int Fret { get; set; }
        public LeftHandFingerIndex? FingerIndex { get; set; }
        public bool IsImportant { get; set; }

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
