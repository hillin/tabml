using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Player;

namespace TabML.Core.Document
{
    public interface IChordFingeringNote
    {
        int Fret { get; }
        LeftHandFingerIndex? FingerIndex { get; }
        bool IsImportant { get; }
    }
}
