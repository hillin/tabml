using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    class BeatRenderer : BeatElementRenderer<Beat>
    {

        private BarRenderer _ownerBar;
        public BarRenderer OwnerBar => _ownerBar ?? (_ownerBar = BeatElementRenderer.FindOwnerBarRenderer(this));

        public BeatRenderer(ElementRenderer owner, Beat beat)
            : base(owner, beat)
        {
        }


        public override void Render(BeamSlope beamSlope)
        {
            this.Render(beamSlope, null);
        }

        private void Render(BeamSlope beamSlope, Beat tieTarget)
        {
            if (this.Element.IsTied)
            {
                if (this.Element.PreviousBeat == null)
                {
                    // todo: raise an error?
                }
                else
                {
                    this.Root.GetRenderer<Beat, BeatRenderer>(this.Element.PreviousBeat)
                        .Render(beamSlope, tieTarget ?? this.Element);
                }

                return;
            }

            this.DrawHead(this.Element, beamSlope, tieTarget);

            if (!this.Element.IsRest)
                this.DrawStemAndFlag(this.Element, beamSlope, tieTarget);

            var noteValue = tieTarget?.NoteValue ?? this.Element.NoteValue;
            if (noteValue.Augment != NoteValueAugment.None)
            {
                this.RenderingContext.DrawNoteValueAugment(noteValue.Augment,
                                                           noteValue.Base,
                                                           this.Element.OwnerColumn.GetPosition(this.RenderingContext),
                                                           this.Element.GetNoteStrings(),
                                                           this.Element.VoicePart);
            }

            if (!this.Element.IsTied
                && !this.Element.IsRest
                && tieTarget == null) // only draw ties for the beat itself
            {
                var current = this.Element;
                var next = current.NextBeat;
                while (next != null && next.IsTied)
                {
                    foreach (var note in this.Element.Notes)
                    {
                        var tieFrom = current.OwnerColumn.GetPositionInRow(this.RenderingContext)
                                      + current.GetAlternationOffset(this.RenderingContext);
                        var tieTo = next.OwnerColumn.GetPositionInRow(this.RenderingContext)
                                    + next.GetAlternationOffset(this.RenderingContext);
                        this.RenderingContext.Owner.DrawTie(tieFrom,
                                                            tieTo,
                                                            note.String,
                                                            this.Element.VoicePart, null, 0);
                    }

                    current = next;
                    next = current.NextBeat;
                }
            }
        }

        public void DrawHead(Beat beat, BeamSlope beamSlope, Beat tieTarget = null)
        {
            var targetBeat = tieTarget ?? beat;
            if (beat.IsRest)
            {
                var targetNoteValue = targetBeat.NoteValue;
                var position = targetBeat.OwnerColumn.GetPosition(this.RenderingContext);
                this.RenderingContext.DrawRest(targetNoteValue.Base, position, beat.VoicePart);
                if (beat.OwnerBeam == null && targetNoteValue.Tuplet != null)
                {
                    this.RenderingContext.DrawTupletForRest(targetNoteValue.Tuplet.Value, position, beat.VoicePart);
                }
            }
            else
            {
                foreach (var note in beat.Notes)
                {
                    var noteRenderer = new NoteRenderer(this, note);
                    noteRenderer.RenderingContext = this.RenderingContext;
                    noteRenderer.Render(targetBeat, beamSlope);
                }
            }
        }

        private void DrawStemAndFlag(Beat beat, BeamSlope beamSlope, Beat tieTarget)
        {
            var stemTailPosition = 0.0;
            var targetBeat = tieTarget ?? beat;
            var position = targetBeat.OwnerColumn.GetPosition(this.RenderingContext)
                           + beat.GetAlternationOffset(this.RenderingContext, tieTarget: tieTarget);

            if (targetBeat.NoteValue.Base <= BaseNoteValue.Half)
            {
                double from;
                this.RenderingContext.GetStemOffsetRange(beat.GetNearestStringIndex(), beat.VoicePart, out from,
                                      out stemTailPosition);

                if (beamSlope != null)
                    stemTailPosition = beamSlope.GetY(position);

                this.RenderingContext.DrawStem(position, from, stemTailPosition);
            }

            if (targetBeat.OwnerBeam == null)
            {
                this.RenderingContext.DrawFlag(targetBeat.NoteValue.Base, position, stemTailPosition, beat.VoicePart);

                if (targetBeat.NoteValue.Tuplet != null)
                    this.RenderingContext.DrawTuplet(targetBeat.NoteValue.Tuplet.Value, position, stemTailPosition,
                                  beat.VoicePart);
            }
            else
            {
                var baseNoteValue = targetBeat.NoteValue.Base;
                var isLastOfBeam = targetBeat == targetBeat.OwnerBeam.Elements[targetBeat.OwnerBeam.Elements.Count - 1];
                while (baseNoteValue != targetBeat.OwnerBeam.BeatNoteValue)
                {
                    this.DrawSemiBeam(baseNoteValue, position, beat.VoicePart, beamSlope, isLastOfBeam);
                    baseNoteValue = baseNoteValue.Double();
                }
            }
        }

        private void DrawSemiBeam(BaseNoteValue noteValue, double position,
                                  VoicePart voicePart, BeamSlope beamSlope, bool isLastOfBeam)
        {
            double x0, x1;
            if (isLastOfBeam)
            {
                x0 = position - this.RenderingContext.Style.SemiBeamWidth;
                x1 = position;
            }
            else
            {
                x0 = position;
                x1 = position + this.RenderingContext.Style.SemiBeamWidth;
            }

            this.RenderingContext.DrawBeam(noteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), voicePart);
        }
    }
}
