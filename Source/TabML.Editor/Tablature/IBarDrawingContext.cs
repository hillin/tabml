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
        void DrawFretNumber(int stringIndex, string fretNumber, double position, bool isHalfOrLonger);
        void FinishHorizontalBarLines(double width);
        void DrawBarLine(OpenBarLine line, double position);
        void DrawBarLine(CloseBarLine line, double position);
        void DrawStem(BaseNoteValue noteValue, double position, VoicePart voicePart);
        void DrawFlag(BaseNoteValue noteValue, double position, VoicePart voicePart);
        void DrawSemiBeam(BaseNoteValue noteValue, double position, VoicePart voicePart, bool isLastOfBeam);
        void DrawNoteValueAugment(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, int[] strings, double position, VoicePart voicePart);
        void DrawNoteValueAugmentOnBeam(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, double position, VoicePart voicePart);
        void DrawBeam(BaseNoteValue noteValue, double from, double to, VoicePart voicePart);
        void DrawRest(BaseNoteValue noteValue, double position, VoicePart voicePart);
        
    }
}
