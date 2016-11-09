using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
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
        void DrawStem(double position, VoicePart voicePart);
        void DrawFlag(NoteValue noteValue, double position, VoicePart voicePart);
        void DrawHalfBeam(BaseNoteValue noteValue, double position, VoicePart voicePart, bool isLastOfBeam);
        void DrawNoteValueAugment(NoteValueAugment noteValueAugment, double position, VoicePart voicePart);
        void DrawBeam(BaseNoteValue beatNoteValue, double from, double to);
    }
}
