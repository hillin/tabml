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
    }
}
