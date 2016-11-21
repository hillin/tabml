using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    class BeatRenderer
    {
        public void Render(BarDrawingContext drawingContext, Beat beat, BeamSlope beamSlope)
        {
            this.Render(drawingContext, beat, beamSlope, null);
        }

        public void Render(BarDrawingContext drawingContext, Beat beat, BeamSlope beamSlope, Beat asTiedFor)
        {
            if (beat.IsTied)
            {
                if (beat.PreviousBeat == null)
                {
                    // todo: handle cross-bar ties
                }
                else
                {
                    new BeatRenderer().Render(drawingContext, beat.PreviousBeat, beamSlope, asTiedFor ?? beat);
                }

                return;
            }

            this.DrawHead(drawingContext, beat, beamSlope, asTiedFor);

            if (!beat.IsRest)
                this.DrawStemAndFlag(drawingContext, beat, beamSlope, asTiedFor);

            var noteValue = asTiedFor?.NoteValue ?? beat.NoteValue;
            if (noteValue.Augment != NoteValueAugment.None)
            {
                drawingContext.DrawNoteValueAugment(noteValue.Augment,
                                                    noteValue.Base,
                                                    beat.Column.GetPosition(drawingContext),
                                                    beat.GetNoteStrings(),
                                                    beat.VoicePart);
            }

            if (!beat.IsTied && !beat.IsRest)
            {
                var current = beat;
                var next = current.NextBeat;
                while (next != null && next.IsTied)
                {
                    foreach (var note in next.Notes)
                    {
                        drawingContext.DrawTie(current.Column.GetPosition(drawingContext),
                                               next.Column.GetPosition(drawingContext),
                                               note.String,
                                               beat.VoicePart, null, 0);
                    }

                    current = next;
                    next = current.NextBeat;
                }
            }
        }

        public void DrawHead(BarDrawingContext drawingContext, Beat beat, BeamSlope beamSlope, Beat asTiedFor = null)
        {
            var targetBeat = asTiedFor ?? beat;
            if (beat.IsRest)
            {
                var targetNoteValue = targetBeat.NoteValue;
                var position = targetBeat.Column.GetPosition(drawingContext);
                drawingContext.DrawRest(targetNoteValue.Base, position, beat.VoicePart);
                if (beat.OwnerBeam == null && targetNoteValue.Tuplet != null)
                {
                    drawingContext.DrawTupletForRest(targetNoteValue.Tuplet.Value, position, beat.VoicePart);
                }
            }
            else
            {
                foreach (var note in beat.Notes)
                {
                    new NoteRenderer().Render(drawingContext, note, beat, beamSlope);
                }
            }
        }

        private void DrawStemAndFlag(BarDrawingContext drawingContext, Beat beat, BeamSlope beamSlope, Beat asTiedFor)
        {
            double stemTailPosition;
            var targetBeat = asTiedFor ?? beat;
            var position = targetBeat.Column.GetPosition(drawingContext);

            this.DrawStem(drawingContext, targetBeat.NoteValue, beat, beamSlope, out stemTailPosition);

            if (targetBeat.OwnerBeam == null)
            {
                drawingContext.DrawFlag(targetBeat.NoteValue.Base, position, stemTailPosition, beat.VoicePart);

                if (targetBeat.NoteValue.Tuplet != null)
                    drawingContext.DrawTuplet(targetBeat.NoteValue.Tuplet.Value, position, stemTailPosition,
                                              beat.VoicePart);
            }
            else
            {
                var baseNoteValue = targetBeat.NoteValue.Base;
                var isLastOfBeam = targetBeat == targetBeat.OwnerBeam.Elements[targetBeat.OwnerBeam.Elements.Count - 1];
                while (baseNoteValue != targetBeat.OwnerBeam.BeatNoteValue)
                {
                    this.DrawSemiBeam(drawingContext, baseNoteValue, position, beat.VoicePart, beamSlope, isLastOfBeam);
                    baseNoteValue = baseNoteValue.Double();
                }
            }
        }

        private void DrawSemiBeam(BarDrawingContext drawingContext, BaseNoteValue noteValue, double position,
                                  VoicePart voicePart, BeamSlope beamSlope, bool isLastOfBeam)
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

            drawingContext.DrawBeam(noteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), voicePart);
        }

        private void DrawStem(BarDrawingContext drawingContext, NoteValue noteValue, Beat beat, BeamSlope beamSlope, out double stemTailPosition)
        {
            if (noteValue.Base > BaseNoteValue.Half)
            {
                stemTailPosition = 0;
                return;
            }

            double from;
            drawingContext.GetStemOffsetRange(beat.GetNearestStringIndex(), beat.VoicePart, out from, out stemTailPosition);

            var position = beat.Column.GetPosition(drawingContext);

            if (beamSlope != null)
                stemTailPosition = beamSlope.GetY(position);

            drawingContext.DrawStem(position, from, stemTailPosition);
        }

    }
}
