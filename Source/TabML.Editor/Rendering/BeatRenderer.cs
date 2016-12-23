using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly List<NoteRenderer> _noteRenderers;

        public BeatRenderer(ElementRenderer owner, Beat beat)
            : base(owner, beat)
        {
            _noteRenderers = new List<NoteRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            if (this.Element.Notes != null)
                _noteRenderers.AddRange(this.Element.Notes.Select(n => new NoteRenderer(this, n)));

            _noteRenderers.Initialize();
        }

        protected override void OnAssignRenderingContext(BarRenderingContext renderingContext)
        {
            base.OnAssignRenderingContext(renderingContext);
            _noteRenderers.AssignRenderingContexts(renderingContext);
        }

        public override async Task Render()
        {
            await this.Render(null);
        }

        private async Task Render(Beat tieTarget)
        {
            if (this.Element.IsTied)
            {
                if (tieTarget == null        // don't draw tied beat if we are drawing for a tied target
                    && !this.Element.IsRest)
                {
                    await this.RenderTiedBeat();
                }
                return;
            }

            var targetBeat = tieTarget ?? this.Element;
            var targetBeatRenderer = this.Root.GetRenderer<Beat, BeatRenderer>(targetBeat);

            var beamSlope = targetBeatRenderer.RenderingContext.GetBeamSlope(targetBeat);

            await this.DrawHead(beamSlope, targetBeat);

            if (!this.Element.IsRest)
                this.DrawStemAndFlag(beamSlope, targetBeat, tieTarget);

            var noteValue = tieTarget?.NoteValue ?? this.Element.NoteValue;
            if (noteValue.Augment != NoteValueAugment.None)
            {
                this.RenderingContext.DrawNoteValueAugment(noteValue.Augment,
                                                           noteValue.Base,
                                                           this.Element.OwnerColumn.GetPosition(this.RenderingContext),
                                                           this.Element.GetNoteStrings(),
                                                           this.Element.VoicePart);
            }
        }

        private async Task RenderTiedBeat()
        {
            // ensure we are at the end of the tie
            if (this.Element.NextBeat != null && this.Element.NextBeat.IsTied)
                return;

            // collect chained-tied beats
            var tiedBeats = new LinkedList<Beat>();
            var current = this.Element;
            do
            {
                tiedBeats.AddFirst(current);
                current = current.PreviousBeat;
            } while (current != null && current.IsTied);

            // this is where all the beats are tied to
            var headBeat = current;

            if (headBeat == null)
            {
                // todo: raise an error?
                return;
            }

            var headBeatRenderer = this.Root.GetRenderer<Beat, BeatRenderer>(headBeat);

            var from = headBeat;

            var defaultTiePosition = this.Element.GetTiePosition();
            var stringIndices = headBeat.Notes.Select(n => n.String).ToArray();

            foreach (var to in tiedBeats)
            {
                await headBeatRenderer.Render(to);

                var tiePosition = to.TiePosition ?? defaultTiePosition;
                NoteConnectionRenderer.DrawTie(this.Root, from, to, stringIndices, tiePosition);

                from = to;
            }
        }

        public async Task DrawHead(BeamSlope beamSlope, Beat targetBeat)
        {
            var renderingContext = this.GetRenderingContext(targetBeat);

            if (this.Element.IsRest)
            {
                var targetNoteValue = targetBeat.NoteValue;
                var position = targetBeat.OwnerColumn.GetPosition(renderingContext);
                renderingContext.DrawRest(targetNoteValue.Base, position, this.Element.VoicePart);
                if (this.Element.OwnerBeam == null && targetNoteValue.Tuplet != null)
                {
                    renderingContext.DrawTupletForRest(targetNoteValue.Tuplet.Value, position, this.Element.VoicePart);
                }
            }
            else
            {
                foreach (var renderer in _noteRenderers)
                    await renderer.Render(targetBeat, beamSlope);
            }
        }

        private void DrawStemAndFlag(BeamSlope beamSlope, Beat targetBeat, Beat tieTarget)
        {
            var stemTailPosition = 0.0;

            var renderingContext = this.GetRenderingContext(targetBeat);

            var position = targetBeat.OwnerColumn.GetPosition(renderingContext)
                           + targetBeat.GetAlternationOffset(renderingContext);

            if (targetBeat.NoteValue.Base <= BaseNoteValue.Half)
            {
                double from;
                renderingContext.GetStemOffsetRange(this.Element.GetNearestStringIndex(),
                                                    this.Element.VoicePart,
                                                    out from,
                                                    out stemTailPosition);

                if (beamSlope != null)
                    stemTailPosition = beamSlope.GetY(position);

                renderingContext.DrawStem(this.Element.VoicePart, position, from, stemTailPosition);
            }

            if (targetBeat.OwnerBeam == null)
            {
                renderingContext.DrawFlag(targetBeat.NoteValue.Base, position, stemTailPosition,
                                          this.Element.VoicePart);

                if (targetBeat.NoteValue.Tuplet != null)
                    renderingContext.DrawTuplet(targetBeat.NoteValue.Tuplet.Value, position, stemTailPosition,
                                                this.Element.VoicePart);
            }
            else
            {
                var baseNoteValue = targetBeat.NoteValue.Base;
                var isLastOfBeam = targetBeat == targetBeat.OwnerBeam.Elements[targetBeat.OwnerBeam.Elements.Count - 1];
                while (baseNoteValue != targetBeat.OwnerBeam.BeatNoteValue)
                {
                    this.DrawSemiBeam(baseNoteValue, position, this.Element.VoicePart, beamSlope, isLastOfBeam);
                    baseNoteValue = baseNoteValue.Double();
                }
            }
        }

        private BarRenderingContext GetRenderingContext(Beat beat)
        {
            return this.Root.GetRenderer<Beat, BeatRenderer>(beat).RenderingContext;
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
