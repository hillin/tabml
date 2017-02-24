//#define DRAW_STRUM_TECHNIQUE_ORNAMENTS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature;
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

            var notes = this.Element.NotesDefiner.Notes;
            if (notes != null)
            {
                _noteRenderers.AddRange(this.Element.IsTied
                                            ? notes.Select(n => new NoteRenderer(this, n.CloneAsTied()))
                                            : notes.Select(n => new NoteRenderer(this, n)));
            }

            _noteRenderers.Initialize();
        }

        public override async Task Render()
        {
            await this.DrawHead();
            if (!this.Element.IsRest)
                await this.DrawStemAndFlag();

            await this.DrawBeatOrnaments();
        }

        public override Task PostRender()
        {
            return Task.FromResult(0);
        }

        private async Task DrawBeatOrnaments()
        {
            var beatPosition = this.GetStemPosition();

            switch (this.Element.StrumTechnique)
            {
                case StrumTechnique.Rasgueado:
                    await this.RenderingContext.DrawRasgueadoText(this.Element.VoicePart, beatPosition);
                    break;

#if DRAW_STRUM_TECHNIQUE_ORNAMENTS
                case StrumTechnique.PickstrokeDown:
                    await this.RenderingContext.DrawPickstrokeDown(this.Element.VoicePart, beatPosition);
                    break;
                case StrumTechnique.PickstrokeUp:
                    await this.RenderingContext.DrawPickstrokeUp(this.Element.VoicePart, beatPosition);
                    break;
                case StrumTechnique.BrushDown:
                    await this.RenderingContext.DrawBrushDown(this.Element.VoicePart, beatPosition);
                    break;
                case StrumTechnique.BrushUp:
                    await this.RenderingContext.DrawBrushUp(this.Element.VoicePart, beatPosition);
                    break;
                case StrumTechnique.ArpeggioDown:
                    await this.RenderingContext.DrawArpeggioDown(this.Element.VoicePart, beatPosition);
                    break;
                case StrumTechnique.ArpeggioUp:
                    await this.RenderingContext.DrawArpeggioUp(this.Element.VoicePart, beatPosition);
                    break;
#endif
            }

            switch (this.Element.Accent)
            {
                case BeatAccent.Accented:
                    await this.RenderingContext.DrawAccented(this.Element.VoicePart, beatPosition);
                    break;
                case BeatAccent.HeavilyAccented:
                    await this.RenderingContext.DrawHeavilyAccented(this.Element.VoicePart, beatPosition);
                    break;
            }

            switch (this.Element.DurationEffect)
            {
                case BeatDurationEffect.Fermata:
                    await this.RenderingContext.DrawFermata(this.Element.VoicePart, beatPosition);
                    break;
                case BeatDurationEffect.Staccato:
                    await this.RenderingContext.DrawStaccato(this.Element.VoicePart, beatPosition);
                    break;
                case BeatDurationEffect.Tenuto:
                    await this.RenderingContext.DrawTenuto(this.Element.VoicePart, beatPosition);
                    break;
            }

            switch (this.Element.EffectTechnique)
            {
                case BeatEffectTechnique.Trill:
                    await this.RenderingContext.DrawTrill(this.Element.VoicePart, beatPosition);
                    break;
                case BeatEffectTechnique.Tremolo:
                    await this.RenderingContext.DrawTremolo(this.Element.VoicePart, beatPosition);
                    break;
            }
        }

        private double GetRestRenderStringSpaceIndex()
        {
            if (this.Element.OwnerBar.HasSingularVoice())
                return this.RenderingContext.Style.StringCount / 2.0;

            return this.Element.VoicePart == VoicePart.Treble ? 0 : this.RenderingContext.Style.StringCount - 1;
        }

        public async Task DrawHead()
        {
            if (this.Element.IsRest)
            {
                var position = this.Element.OwnerColumn.GetPosition(this.RenderingContext);
                var restStringIndex = this.GetRestRenderStringSpaceIndex();
                var restBounds = await this.RenderingContext.DrawRest(this.Element.NoteValue.Base, position, restStringIndex);

                this.RenderingContext.DrawNoteValueAugment(this.Element.NoteValue,
                                                           restBounds.Right - this.RenderingContext.Location.X,
                                                           restStringIndex);

                if (this.Element.OwnerBeam == null && this.Element.NoteValue.Tuplet != null)
                {
                    await this.RenderingContext.DrawTuplet(this.Element.NoteValue.Tuplet.Value, position, this.Element.GetStemRenderVoicePart());
                }
            }
            else
            {
                var beatRenderingContext = new BeatRenderingContext(this.RenderingContext);

                _noteRenderers.AssignRenderingContexts(beatRenderingContext);

                foreach (var renderer in _noteRenderers.OrderBy(n => n.Element.Fret))
                {
                    await renderer.Render(this.RenderingContext.GetBeamSlope(this.Element));
                }

                // don't draw A.H. text or connection instructions for tied beat
                if (!this.Element.IsTied)
                {
                    if (beatRenderingContext.ArtificialHarmonicFrets.Count > 0)
                    {
                        var position = this.Element.OwnerColumn.GetPosition(this.RenderingContext);

                        if (beatRenderingContext.ArtificialHarmonicFrets.All(n => n.IsOn12thFret))
                            await this.RenderingContext.DrawArtificialHarmonicText(this.Element.VoicePart, position, "A.H.");
                        else
                        {
                            var orderedFrets = this.Element.VoicePart == VoicePart.Treble
                                ? beatRenderingContext.ArtificialHarmonicFrets.OrderByDescending(f => f.StringIndex)
                                : beatRenderingContext.ArtificialHarmonicFrets.OrderBy(f => f.StringIndex);

                            foreach (var fretInfo in orderedFrets)
                            {
                                var text = fretInfo.IsOn12thFret ? "A.H." : $"A.H. ({fretInfo.ArtificialHarmonicFret})";
                                await this.RenderingContext.DrawArtificialHarmonicText(this.Element.VoicePart, position, text);
                            }
                        }
                    }

                    // todo: replace magic number
                    const double tolerance = 15;
                    var instructionGroups = beatRenderingContext.ConnectionInstructions.GroupBy(c => Math.Round(c.Position / tolerance) * tolerance);
                    foreach (var group in instructionGroups)
                    {
                        var instructions = group.ToArray();
                        var position = instructions.Average(i => i.Position);
                        var firstInstruction = instructions[0].Instruction;

                        if (instructions.All(i => i.Instruction == firstInstruction))
                        {
                            await this.RenderingContext.DrawConnectionInstruction(this.Element.VoicePart, position, firstInstruction);
                        }
                        else
                        {
                            var orderedInstructions = this.Element.VoicePart == VoicePart.Treble ? instructions.OrderByDescending(i => i.StringIndex) : instructions.OrderBy(i => i.StringIndex);
                            foreach (var instructionInfo in orderedInstructions)
                            {
                                await this.RenderingContext.DrawConnectionInstruction(this.Element.VoicePart, position, instructionInfo.Instruction);
                            }
                        }
                    }
                }
            }
        }


        private async Task DrawStemAndFlag()
        {
            var stemTailPosition = 0.0;

            var position = this.GetStemPosition();
            var stemVoicePart = this.Element.GetStemRenderVoicePart();

            var notesDefinerBeat = this.Element.NotesDefiner;

            var beamSlope = this.RenderingContext.GetBeamSlope(this.Element);

            var noteValue = this.Element.NoteValue;
            if (noteValue.Base <= BaseNoteValue.Half)
            {
                double from;
                this.RenderingContext.GetStemOffsetRange(this.Element.OwnerColumn.ColumnIndex, notesDefinerBeat.GetOutmostStringIndex(), stemVoicePart, out from, out stemTailPosition);

                if (beamSlope != null)
                    stemTailPosition = beamSlope.GetY(position);

                this.RenderingContext.DrawStem(stemVoicePart, position, from, stemTailPosition);
            }

            var ownerBeam = this.Element.OwnerBeam;
            if (ownerBeam == null)
            {
                await this.RenderingContext.DrawFlag(noteValue.Base, position, stemTailPosition, stemVoicePart);

                if (noteValue.Tuplet != null)
                    await this.RenderingContext.DrawTuplet(noteValue.Tuplet.Value, position, stemVoicePart);
            }
            else
            {
                var baseNoteValue = noteValue.Base;
                var isLastOfBeam = this.Element == ownerBeam.Elements[ownerBeam.Elements.Count - 1];
                while (baseNoteValue != ownerBeam.BeatNoteValue)
                {
                    await this.DrawSemiBeam(baseNoteValue, stemVoicePart, beamSlope, isLastOfBeam);
                    baseNoteValue = baseNoteValue.Double();
                }
            }
        }

        private double GetStemPosition()
        {
            return this.GetNoteRenderingPosition();
        }

        private async Task DrawSemiBeam(BaseNoteValue noteValue, VoicePart voicePart, BeamSlope beamSlope, bool isLastOfBeam)
        {
            BeatRenderer beatRenderer1, beatRenderer2;
            if (isLastOfBeam)
            {
                beatRenderer1 = this.Root.GetRenderer<Beat, BeatRenderer>(this.Element.PreviousBeat);
                beatRenderer2 = this;
            }
            else
            {
                beatRenderer1 = this;
                beatRenderer2 = this.Root.GetRenderer<Beat, BeatRenderer>(this.Element.NextBeat);
            }

            var position1 = beatRenderer1.GetStemPosition();
            var position2 = beatRenderer2.GetStemPosition();

            var beamWidth = Math.Min(this.RenderingContext.Style.MaximumSemiBeamWidth, (position2 - position1) / 2);

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

        public double GetStemTailPosition()
        {
            double from, to;
            this.RenderingContext.GetStemOffsetRange(this.Element.OwnerColumn.ColumnIndex, this.Element.GetOutmostStringIndex(), this.Element.GetStemRenderVoicePart(), out from, out to);
            return this.Element.GetStemRenderVoicePart() == VoicePart.Treble ? Math.Min(from, to) : Math.Max(from, to);
        }

        public double GetNoteAlternationOffset(int? stringIndex = null)
        {
            var column = this.RenderingContext.ColumnRenderingInfos[this.Element.OwnerColumn.ColumnIndex];

            if (column.HasBrushlikeTechnique && column.MatchesChord)    // in this case we will just draw the technique directly
                return 0;

            var hasHarmonics = this.Element.NotesDefiner.Notes.Any(n => n.IsHarmonics);
            var ratio = column.GetNoteAlternationOffsetRatio(stringIndex ?? this.Element.GetOutmostStringIndex());
            return this.RenderingContext.GetNoteAlternationOffset(ratio, hasHarmonics)
                + column.BrushlikeTechniqueSize
                + this.RenderingContext.Style.BrushlikeTechniqueMargin;
        }

        public double GetNoteRenderingPosition(int? stringIndex = null)
        {
            var column = this.RenderingContext.ColumnRenderingInfos[this.Element.OwnerColumn.ColumnIndex];
            return column.Position + this.GetNoteAlternationOffset(stringIndex);
        }
    }
}
