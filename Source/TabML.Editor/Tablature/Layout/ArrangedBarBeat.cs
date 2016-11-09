using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    [DebuggerDisplay("{DebuggerDisplay, nq}")]
    class ArrangedBarBeat : IBeatElement
    {
        public PreciseDuration Position { get; set; }
        public int ColumnIndex { get; set; }
        public VoicePart VoicePart { get; }
        public Beat Beat { get; }
        public ArrangedBeam OwnerBeam { get; internal set; }

        public ArrangedBarBeat(Beat beat, VoicePart voicePart)
        {
            this.Beat = beat;
            this.VoicePart = voicePart;
        }

        public PreciseDuration GetDuration()
        {
            return this.Beat.GetDuration();
        }

        public int GetBeginColumnIndex() => this.ColumnIndex;

        public int GetEndColumnIndex() => this.ColumnIndex;

#if DEBUG
        private string DebuggerDisplay => $"Beat: {this.Beat.NoteValue.DebuggerDisplay}";
#endif

        public void DrawHead(IBarDrawingContext drawingContext, double position, double width)
        {
            foreach (var note in this.Beat.Notes)
            {
                drawingContext.DrawFretNumber(note.String - 1, note.Fret.ToString(), position + width / 2);
            }
        }

        void IBeatElement.Draw(IBarDrawingContext drawingContext, double[] columnPositions)
        {
            var position = columnPositions[this.ColumnIndex];
            drawingContext.DrawStem(position, this.VoicePart);
            if (this.OwnerBeam == null)
                drawingContext.DrawFlag(this.Beat.NoteValue, position, this.VoicePart);
            else
            {
                var baseNoteValue = this.Beat.NoteValue.Base;
                while (baseNoteValue != this.OwnerBeam.BeatNoteValue)
                {
                    drawingContext.DrawHalfBeam(baseNoteValue, position, this.VoicePart,
                                                this != this.OwnerBeam.Elements[this.OwnerBeam.Elements.Count - 1]);
                    baseNoteValue = baseNoteValue.Double();
                }

                if (this.Beat.NoteValue.Augment != NoteValueAugment.None)
                    drawingContext.DrawNoteValueAugment(this.Beat.NoteValue.Augment, position, this.VoicePart);
            }

        }
    }
}
