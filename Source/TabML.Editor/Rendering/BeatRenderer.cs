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
                await this.DrawStemAndFlag(beamSlope, targetBeat, tieTarget);

            this.DrawNoteValueAugment(tieTarget);
            await this.DrawBeatOrnaments(tieTarget);
        }

        private async Task DrawBeatOrnaments(Beat tieTarget)
        {
            var targetBeat = tieTarget ?? this.Element;
            var renderingContext = this.Root.GetRenderer<Beat, BeatRenderer>(targetBeat).RenderingContext;
            var beatPosition = this.GetStemPosition(targetBeat);
            
            switch (targetBeat.StrumTechnique)
            {
                case StrumTechnique.Rasgueado:
                    await renderingContext.DrawRasgueadoText(targetBeat.VoicePart, beatPosition);
                    break;
                case StrumTechnique.PickstrokeDown:
                    await renderingContext.DrawPickstrokeDown(targetBeat.VoicePart, beatPosition);
                    break;
                case StrumTechnique.PickstrokeUp:
                    await renderingContext.DrawPickstrokeUp(targetBeat.VoicePart, beatPosition);
                    break;
                case StrumTechnique.BrushDown:
                    await renderingContext.DrawBrushDown(targetBeat.VoicePart, beatPosition);
                    break;
                case StrumTechnique.BrushUp:
                    await renderingContext.DrawBrushUp(targetBeat.VoicePart, beatPosition);
                    break;
                case StrumTechnique.ArpeggioDown:
                    await renderingContext.DrawArpeggioDown(targetBeat.VoicePart, beatPosition);
                    break;
                case StrumTechnique.ArpeggioUp:
                    await renderingContext.DrawArpeggioUp(targetBeat.VoicePart, beatPosition);
                    break;
            }

            switch (targetBeat.Accent)
            {
                case BeatAccent.Accented:
                    await renderingContext.DrawAccented(targetBeat.VoicePart, beatPosition);
                    break;
                case BeatAccent.HeavilyAccented:
                    await renderingContext.DrawHeavilyAccented(targetBeat.VoicePart, beatPosition);
                    break;
            }

            switch (targetBeat.DurationEffect)
            {
                case BeatDurationEffect.Fermata:
                    await renderingContext.DrawFermata(targetBeat.VoicePart, beatPosition);
                    break;
                case BeatDurationEffect.Staccato:
                    await renderingContext.DrawStaccato(targetBeat.VoicePart, beatPosition);
                    break;
                case BeatDurationEffect.Tenuto:
                    await renderingContext.DrawTenuto(targetBeat.VoicePart, beatPosition);
                    break;
            }

            switch (targetBeat.EffectTechnique)
            {
                case BeatEffectTechnique.Trill:
                    await renderingContext.DrawTrill(targetBeat.VoicePart, beatPosition);
                    break;
                case BeatEffectTechnique.Tremolo:
                    await renderingContext.DrawTremolo(targetBeat.VoicePart, beatPosition);
                    break;
            }
        }

        private void DrawNoteValueAugment(Beat tieTarget)
        {
            var noteValue = tieTarget?.NoteValue ?? this.Element.NoteValue;
            if (noteValue.Augment != NoteValueAugment.None)
            {
                this.RenderingContext.DrawNoteValueAugment(noteValue.Augment, noteValue.Base, this.Element.OwnerColumn.GetPosition(this.RenderingContext), this.Element.GetNoteStrings(), this.Element.VoicePart);
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
                await NoteConnectionRenderer.DrawTie(this.Root, @from, to, stringIndices, tiePosition);

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
                    await renderingContext.DrawTuplet(targetNoteValue.Tuplet.Value, position, this.Element.VoicePart);
                }
            }
            else
            {
                var beatRenderingContext = new BeatRenderingContext(renderingContext);
                foreach (var renderer in _noteRenderers.OrderBy(n => n.Element.Fret))
                {
                    _noteRenderers.AssignRenderingContexts(beatRenderingContext);
                    await renderer.Render(targetBeat, beamSlope);
                }

                // only draw A.H. text and connection instructions for self
                if (targetBeat == this.Element)
                {
                    if (beatRenderingContext.ArtificialHarmonicFrets.Count > 0)
                    {
                        var position = this.Element.OwnerColumn.GetPosition(this.RenderingContext);

                        if (beatRenderingContext.ArtificialHarmonicFrets.All(n => n.IsOn12thFret))
                            await renderingContext.DrawArtificialHarmonicText(this.Element.VoicePart, position, "A.H.");
                        else
                        {
                            var orderedFrets = this.Element.VoicePart == VoicePart.Treble ? beatRenderingContext.ArtificialHarmonicFrets.OrderByDescending(f => f.StringIndex) : beatRenderingContext.ArtificialHarmonicFrets.OrderBy(f => f.StringIndex);

                            foreach (var fretInfo in orderedFrets)
                            {
                                var text = fretInfo.IsOn12thFret ? "A.H." : $"A.H. ({fretInfo.ArtificialHarmonicFret})";
                                await renderingContext.DrawArtificialHarmonicText(this.Element.VoicePart, position, text);
                            }
                        }
                    }

                    // todo: replace magic number
                    const double tolerance = 15;
                    var instructionGroups = beatRenderingContext.ConnectionInstructions.GroupBy(c => Math.Round(c.Position/tolerance)*tolerance);
                    foreach (var group in instructionGroups)
                    {
                        var instructions = group.ToArray();
                        var position = instructions.Average(i => i.Position);
                        var firstInstruction = instructions[0].Instruction;

                        if (instructions.All(i => i.Instruction == firstInstruction))
                        {
                            await renderingContext.DrawConnectionInstruction(this.Element.VoicePart, position, firstInstruction);
                        }
                        else
                        {
                            var orderedInstructions = this.Element.VoicePart == VoicePart.Treble ? instructions.OrderByDescending(i => i.StringIndex) : instructions.OrderBy(i => i.StringIndex);
                            foreach (var instructionInfo in orderedInstructions)
                            {
                                await renderingContext.DrawConnectionInstruction(this.Element.VoicePart, position, instructionInfo.Instruction);
                            }
                        }
                    }
                }
            }
        }


        private async Task DrawStemAndFlag(BeamSlope beamSlope, Beat targetBeat, Beat tieTarget)
        {
            var stemTailPosition = 0.0;

            var renderingContext = this.GetRenderingContext(targetBeat);

            var position = this.GetStemPosition(targetBeat);

            if (targetBeat.NoteValue.Base <= BaseNoteValue.Half)
            {
                double from;
                renderingContext.GetStemOffsetRange(this.Element.GetNearestStringIndex(), this.Element.VoicePart, out from, out stemTailPosition);

                if (beamSlope != null)
                    stemTailPosition = beamSlope.GetY(position);

                renderingContext.DrawStem(this.Element.VoicePart, position, from, stemTailPosition);
            }

            if (targetBeat.OwnerBeam == null)
            {
                await renderingContext.DrawFlag(targetBeat.NoteValue.Base, position, stemTailPosition, this.Element.VoicePart);

                if (targetBeat.NoteValue.Tuplet != null)
                    await renderingContext.DrawTuplet(targetBeat.NoteValue.Tuplet.Value, position, this.Element.VoicePart);
            }
            else
            {
                var baseNoteValue = targetBeat.NoteValue.Base;
                var isLastOfBeam = targetBeat == targetBeat.OwnerBeam.Elements[targetBeat.OwnerBeam.Elements.Count - 1];
                while (baseNoteValue != targetBeat.OwnerBeam.BeatNoteValue)
                {
                    await this.DrawSemiBeam(targetBeat, baseNoteValue, this.Element.VoicePart, beamSlope, isLastOfBeam);
                    baseNoteValue = baseNoteValue.Double();
                }
            }
        }

        private double GetStemPosition(Beat beat)
        {
            var renderingContext = this.GetRenderingContext(beat);
            return beat.OwnerColumn.GetPosition(renderingContext) + beat.GetAlternationOffset(renderingContext);
        }

        private BarRenderingContext GetRenderingContext(Beat beat)
        {
            return this.Root.GetRenderer<Beat, BeatRenderer>(beat).RenderingContext;
        }

        private async Task DrawSemiBeam(Beat targetBeat, BaseNoteValue noteValue, VoicePart voicePart, BeamSlope beamSlope, bool isLastOfBeam)
        {
            Beat beat1, beat2;
            if (isLastOfBeam)
            {
                beat1 = targetBeat.PreviousBeat;
                beat2 = targetBeat;
            }
            else
            {
                beat1 = targetBeat;
                beat2 = targetBeat.NextBeat;
            }

            var position1 = this.GetStemPosition(beat1);
            var position2 = this.GetStemPosition(beat2);

            var beamWidth = Math.Min(this.RenderingContext.Style.MaximumSemiBeamWidth, (position2 - position1)/2);

            double x0, x1;
            if (isLastOfBeam)
            {
                x0 = position2 - beamWidth;
                x1 = position2;
            }
            else
            {
                x0 = position1;
                x1 = position1 + beamWidth;
            }

            await this.RenderingContext.DrawBeam(noteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), voicePart);
        }
    }
}
