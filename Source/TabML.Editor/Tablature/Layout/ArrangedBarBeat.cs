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
        public ArrangedBarColumn Column { get; set; }
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

#if DEBUG
        private string DebuggerDisplay => $"Beat: {this.Beat.NoteValue.DebuggerDisplay}";
#endif

        public void DrawHead(IBarDrawingContext drawingContext, double position, double width)
        {
            if (this.Beat.IsRest)
            {
                drawingContext.DrawRest(this.Beat.NoteValue.Base, position, this.VoicePart);
                if (this.OwnerBeam == null && this.Beat.NoteValue.Tuplet != null)
                {
                    drawingContext.DrawTupletForRest(this.Beat.NoteValue.Tuplet.Value, position, this.VoicePart);
                }
            }
            else
            {
                foreach (var note in this.Beat.Notes)
                {
                    drawingContext.DrawFretNumber(note.String - 1, note.Fret.ToString(), position,
                                                  this.Column.GetNoteHeadOffset(note.String - 1),
                                                  this.Beat.NoteValue.Base >= BaseNoteValue.Half);
                }
            }
        }

        void IBeatElement.Draw(IBarDrawingContext drawingContext, double[] columnPositions, BeamSlope beamSlope)
        {
            var position = columnPositions[this.Column.ColumnIndex];

            var strings = this.GetNoteStrings();

            if (!this.Beat.IsRest)
                this.DrawStemAndFlag(drawingContext, strings, position, beamSlope);

            if (this.Beat.NoteValue.Augment != NoteValueAugment.None)
            {
                drawingContext.DrawNoteValueAugment(this.Beat.NoteValue.Augment,
                                                    this.Beat.NoteValue.Base,
                                                    position,
                                                    this.GetNoteStrings(),
                                                    this.VoicePart);
            }

        }

        public int[] GetNoteStrings()
        {
            if (this.Beat.Notes == null || this.Beat.Notes.Length == 0)
                return new[] { this.VoicePart == VoicePart.Bass ? 5 : 0 };
            else
                return this.Beat.Notes.Select(n => n.String).ToArray();
        }

        public int GetNearestStringIndex()
        {
            if (this.Beat.Notes == null || this.Beat.Notes.Length == 0)
                return this.VoicePart == VoicePart.Bass ? 5 : 0;

            return this.VoicePart == VoicePart.Bass
                ? this.Beat.Notes.Max(n => n.String) - 1
                : this.Beat.Notes.Min(n => n.String) - 1;
        }

        private void DrawStemAndFlag(IBarDrawingContext drawingContext, int[] strings, double position, BeamSlope beamSlope)
        {
            double stemTailPosition;
            this.DrawStem(drawingContext, position, beamSlope, out stemTailPosition);

            if (this.OwnerBeam == null)
            {
                drawingContext.DrawFlag(this.Beat.NoteValue.Base, position, stemTailPosition, this.VoicePart);

                if (this.Beat.NoteValue.Tuplet != null)
                    drawingContext.DrawTuplet(this.Beat.NoteValue.Tuplet.Value, position, stemTailPosition,
                                              this.VoicePart);
            }
            else
            {
                var baseNoteValue = this.Beat.NoteValue.Base;
                var isLastOfBeam = this == this.OwnerBeam.Elements[this.OwnerBeam.Elements.Count - 1];
                while (baseNoteValue != this.OwnerBeam.BeatNoteValue)
                {
                    this.DrawSemiBeam(drawingContext, baseNoteValue, position, beamSlope, isLastOfBeam);
                    baseNoteValue = baseNoteValue.Double();
                }
            }
        }

        private void DrawSemiBeam(IBarDrawingContext drawingContext, BaseNoteValue noteValue, double position, BeamSlope beamSlope, bool isLastOfBeam)
        {
            double x0, x1;
            if (isLastOfBeam)
            {
                x0 = position - drawingContext.Style.SemiBeamWidth;
                x1 = position;
            }
            else
            {
                x0 = position;
                x1 = position + drawingContext.Style.SemiBeamWidth;
            }

            drawingContext.DrawBeam(noteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), this.VoicePart);
        }

        private void DrawStem(IBarDrawingContext drawingContext, double position, BeamSlope beamSlope, out double stemTailPosition)
        {
            if (this.Beat.NoteValue.Base > BaseNoteValue.Half)
            {
                stemTailPosition = 0;
                return;
            }

            double from;
            drawingContext.GetStemOffsetRange(this.GetNearestStringIndex(), this.VoicePart, out from, out stemTailPosition);

            if (beamSlope != null)
                stemTailPosition = beamSlope.GetY(position);

            drawingContext.DrawStem(position, from, stemTailPosition);
        }


        public double GetStemTailPosition(IBarDrawingContext drawingContext)
        {
            double from, to;
            drawingContext.GetStemOffsetRange(this.GetNearestStringIndex(), this.VoicePart, out from, out to);
            return this.VoicePart == VoicePart.Treble ? Math.Min(from, to) : Math.Max(from, to);
        }
    }
}
