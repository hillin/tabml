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
    class ArrangedBeat : IBeatElement
    {
        public PreciseDuration Position { get; set; }
        public ArrangedBarColumn Column { get; set; }
        public VoicePart VoicePart { get; }
        public Beat Beat { get; }
        public ArrangedBeam OwnerBeam { get; internal set; }
        public ArrangedBeat PreviousBeat { get; set; }
        public ArrangedBeat NextBeat { get; set; }
        public List<ArrangedNote> Notes { get; }

        public ArrangedBeat(Beat beat, VoicePart voicePart)
        {
            this.Beat = beat;
            this.VoicePart = voicePart;
            this.Notes = new List<ArrangedNote>();
        }

        public PreciseDuration GetDuration()
        {
            return this.Beat.GetDuration();
        }

#if DEBUG
        private string DebuggerDisplay => $"Beat: {this.Beat.NoteValue.DebuggerDisplay}";
#endif

        public void DrawHead(IBarDrawingContext drawingContext, BeamSlope beamSlope, ArrangedBeat asTiedFor = null)
        {
            var beat = asTiedFor ?? this;
            if (this.Beat.IsRest)
            {
                drawingContext.DrawRest(beat.Beat.NoteValue.Base, beat.Column.Position, this.VoicePart);
                if (this.OwnerBeam == null && beat.Beat.NoteValue.Tuplet != null)
                {
                    drawingContext.DrawTupletForRest(beat.Beat.NoteValue.Tuplet.Value, beat.Column.Position, this.VoicePart);
                }
            }
            else
            {
                foreach (var note in this.Notes)
                {
                    note.Draw(drawingContext, beat, beamSlope);
                }
            }
        }


        void IBeatElement.Draw(IBarDrawingContext drawingContext, double[] columnPositions, BeamSlope beamSlope)
        {
            this.Draw(drawingContext, this.Column.Position, beamSlope);
        }

        private void Draw(IBarDrawingContext drawingContext, double position, BeamSlope beamSlope, ArrangedBeat asTiedFor = null)
        {
            if (this.Beat.IsTied)
            {
                if (this.PreviousBeat == null)
                {
                    // todo: handle cross-bar ties
                }
                else
                {
                    this.PreviousBeat.Draw(drawingContext, position, beamSlope, asTiedFor ?? this);
                }

                return;
            }

            this.DrawHead(drawingContext, beamSlope, asTiedFor);

            if (!this.Beat.IsRest)
                this.DrawStemAndFlag(drawingContext, position, beamSlope, asTiedFor);

            var noteValue = asTiedFor?.Beat.NoteValue ?? this.Beat.NoteValue;
            if (noteValue.Augment != NoteValueAugment.None)
            {
                drawingContext.DrawNoteValueAugment(noteValue.Augment,
                                                    noteValue.Base,
                                                    position,
                                                    this.GetNoteStrings(),
                                                    this.VoicePart);
            }

            if (!this.Beat.IsTied && !this.Beat.IsRest)
            {
                var previousBeat = this;
                var beat = previousBeat.NextBeat;
                while (beat != null && beat.Beat.IsTied)
                {
                    foreach (var note in this.Beat.Notes)
                    {
                        drawingContext.DrawTie(previousBeat.Column.Position, beat.Column.Position, note.String,
                                               this.VoicePart, null, 0);
                    }

                    previousBeat = beat;
                    beat = previousBeat.NextBeat;
                }
            }
        }

        public int[] GetNoteStrings()
        {
            if (this.Beat.IsTied)
            {
                if (this.PreviousBeat == null)
                {
                    // todo: handle cross-bar ties
                }
                else
                    return this.PreviousBeat.GetNoteStrings();
            }

            if (this.Beat.Notes == null || this.Beat.Notes.Length == 0)
                return new[] { this.VoicePart == VoicePart.Bass ? 5 : 0 };
            else
                return this.Beat.Notes.Select(n => n.String).ToArray();
        }

        public int GetNearestStringIndex()
        {
            if (this.Beat.IsTied)
            {
                if (this.PreviousBeat == null)
                {
                    // todo: handle cross-bar ties
                }
                else
                    return this.PreviousBeat.GetNearestStringIndex();
            }

            if (this.Beat.Notes == null || this.Beat.Notes.Length == 0)
                return this.VoicePart == VoicePart.Bass ? 5 : 0;

            return this.VoicePart == VoicePart.Bass
                ? this.Beat.Notes.Max(n => n.String)
                : this.Beat.Notes.Min(n => n.String);
        }

        private void DrawStemAndFlag(IBarDrawingContext drawingContext, double position, BeamSlope beamSlope, ArrangedBeat asTiedFor)
        {
            double stemTailPosition;
            var beat = asTiedFor ?? this;

            this.DrawStem(drawingContext, beat.Beat.NoteValue, position, beamSlope, out stemTailPosition);

            if (beat.OwnerBeam == null)
            {
                drawingContext.DrawFlag(beat.Beat.NoteValue.Base, position, stemTailPosition, this.VoicePart);

                if (beat.Beat.NoteValue.Tuplet != null)
                    drawingContext.DrawTuplet(beat.Beat.NoteValue.Tuplet.Value, position, stemTailPosition,
                                              this.VoicePart);
            }
            else
            {
                var baseNoteValue = beat.Beat.NoteValue.Base;
                var isLastOfBeam = beat == beat.OwnerBeam.Elements[beat.OwnerBeam.Elements.Count - 1];
                while (baseNoteValue != beat.OwnerBeam.BeatNoteValue)
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

        private void DrawStem(IBarDrawingContext drawingContext, NoteValue noteValue, double position, BeamSlope beamSlope, out double stemTailPosition)
        {
            if (noteValue.Base > BaseNoteValue.Half)
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
