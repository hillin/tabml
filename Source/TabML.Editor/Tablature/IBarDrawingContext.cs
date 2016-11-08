using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature
{
    interface IBarDrawingContext
    {
        TablatureStyle Style { get; }
        void DrawFretNumber(int stringIndex, string fretNumber, double position);
        void FinishHorizontalBarLines(double width);
        void DrawBarLine(OpenBarLine line, double position);
        void DrawBarLine(CloseBarLine line, double position);
    }
}
