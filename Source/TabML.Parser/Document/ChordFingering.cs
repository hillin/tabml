using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
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
