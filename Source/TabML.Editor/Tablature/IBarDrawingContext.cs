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
        void DrawFretNumber(int stringIndex, string fretNumber, double position, double horizontalOffset, bool isHalfOrLonger);
        void FinishHorizontalBarLines(double width);
        void DrawBarLine(OpenBarLine line, double position);
        void DrawBarLine(CloseBarLine line, double position);
        void DrawStem(double x, double y0, double y1);
        void DrawFlag(BaseNoteValue noteValue, double x, double y, VoicePart voicePart);
        void DrawNoteValueAugment(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, double position, int[] strings, VoicePart voicePart);
        void DrawBeam(BaseNoteValue noteValue, double x0, double y0, double x1, double y1, VoicePart voicePart);
        void DrawRest(BaseNoteValue noteValue, double position, VoicePart voicePart);
        void GetStemOffsetRange(int stringIndex, VoicePart voicePart, out double from, out double to);
        void DrawTuplet(int tuplet, double x, double y, VoicePart voicePart);
        void DrawTupletForRest(int value, double position, VoicePart voicePart);
        void DrawTie(double from, double to, int stringIndex, VoicePart voicePart, string instruction, double instructionY);
    }
}
