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
            if (this.Beat.IsRest)
            {
                drawingContext.DrawRest(this.Beat.NoteValue.Base, position, this.VoicePart);
            }
            else
            {
                foreach (var note in this.Beat.Notes)
                {
                    drawingContext.DrawFretNumber(note.String - 1, note.Fret.ToString(), position,
                                                  this.Beat.NoteValue.Base >= BaseNoteValue.Half);
                }
            }
        }

        void IBeatElement.Draw(IBarDrawingContext drawingContext, double[] columnPositions)
        {
            if (this.Beat.IsRest)
            {
                return; // rest should have been drawn in DrawHead
            }

            var position = columnPositions[this.ColumnIndex];
            drawingContext.DrawStem(this.Beat.NoteValue.Base, position, this.VoicePart);
            if (this.OwnerBeam == null)
            {
                drawingContext.DrawFlag(this.Beat.NoteValue.Base, position, this.VoicePart);
            }
            else
            {
                var baseNoteValue = this.Beat.NoteValue.Base;
                while (baseNoteValue != this.OwnerBeam.BeatNoteValue)
                {
                    drawingContext.DrawSemiBeam(baseNoteValue, position, this.VoicePart,
                                                this == this.OwnerBeam.Elements[this.OwnerBeam.Elements.Count - 1]);
                    baseNoteValue = baseNoteValue.Double();
                }

                if (this.Beat.NoteValue.Augment != NoteValueAugment.None)
                    drawingContext.DrawNoteValueAugmentOnBeam(this.Beat.NoteValue.Augment, this.Beat.NoteValue.Base, position, this.VoicePart);
            }

            if (this.Beat.NoteValue.Augment != NoteValueAugment.None)
                drawingContext.DrawNoteValueAugment(this.Beat.NoteValue.Augment, this.Beat.NoteValue.Base, this.Beat.Notes.Select(n => n.String).ToArray(), position, this.VoicePart);

        }
    }
}
