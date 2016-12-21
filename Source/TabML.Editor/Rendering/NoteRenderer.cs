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
    class NoteRenderer : ElementRenderer<BeatNote, BarRenderingContext>
    {
        public NoteRenderer(ElementRenderer owner, BeatNote element) : base(owner, element)
        {
        }

        /// <remarks>
        /// <para>beat</para> could be different than <c>this.OwnerBeat</c> because we may draw for tied beats
        /// </remarks>
        public void Render(Beat beat, BeamSlope beamSlope)
        {
            var renderingContext = this.Root.GetRenderer<Beat, BeatRenderer>(beat).RenderingContext;

            var x = this.Element.GetRenderPosition(renderingContext, beat);

            var isHalfOrLonger = beat.NoteValue.Base >= BaseNoteValue.Half;
            if (this.Element.EffectTechnique == NoteEffectTechnique.DeadNote)
            {
                renderingContext.DrawDeadNote(this.Element.String, x, isHalfOrLonger);
            }
            else if (this.Element.Fret == BeatNote.UnspecifiedFret)
            {
                renderingContext.DrawPlayAsChordMark(this.Element.String, x, isHalfOrLonger);
            }
            else
            {
                renderingContext.DrawFretNumber(this.Element.String, this.Element.Fret.ToString(), x, isHalfOrLonger);
            }

            if (beat == this.Element.OwnerBeat) // only draw connections if we are drawing for ourselves
            {
                this.DrawPreConnection(renderingContext, beamSlope);
                this.DrawPostConnection(renderingContext, beamSlope);
            }
        }

        private void DrawPostConnection(BarRenderingContext renderingContext, BeamSlope beamSlope)
        {
            switch (this.Element.PostConnection)
            {
                case PostNoteConnection.SlideOutToHigher:
                    this.RenderingContext.DrawGliss(this.Element.OwnerBeat.OwnerColumn.GetPosition(renderingContext),
                                                    this.Element.String,
                                                    GlissDirection.ToHigher,
                                                    this.GetInstructionY(this.Element, beamSlope));
                    break;
                case PostNoteConnection.SlideOutToLower:
                    this.RenderingContext.DrawGliss(this.Element.OwnerBeat.OwnerColumn.GetPosition(renderingContext),
                                                    this.Element.String,
                                                    GlissDirection.ToLower,
                                                    this.GetInstructionY(this.Element, beamSlope));
                    break;
            }
        }

        private void DrawTie(BeatNote note, string instruction, BeamSlope beamSlope)
        {
            if (note.PreConnectedNote != null) // todo: handle cross-bar ties
            {
                var instructionY = string.IsNullOrEmpty(instruction)
                    ? 0.0
                    : this.GetInstructionY(note, beamSlope);

                var tieFrom = note.PreConnectedNote.GetRenderPosition(this.RenderingContext);
                var tieTo = note.GetRenderPosition(this.RenderingContext);
                this.RenderingContext.DrawTie(tieFrom, tieTo, note.String, note.OwnerBeat.VoicePart, instruction, instructionY);
            }
        }

        private double GetInstructionY(BeatNote note, BeamSlope beamSlope)
        {
            if (beamSlope == null)
            {
                return note.OwnerBeat.GetStemTailPosition(this.RenderingContext);
            }

            var instructionX = note.PreConnectedNote != null
                ? (note.OwnerBeat.OwnerColumn.GetPosition(this.RenderingContext) +
                   note.PreConnectedNote.OwnerBeat.OwnerColumn.GetPosition(this.RenderingContext)) / 2
                : note.OwnerBeat.OwnerColumn.GetPosition(this.RenderingContext);
            return beamSlope.GetY(instructionX);
        }

        private void DrawPreConnection(BarRenderingContext renderingContext, BeamSlope beamSlope)
        {
            switch (this.Element.PreConnection)
            {
                case PreNoteConnection.Tie:
                    this.DrawTie(this.Element, null, beamSlope);
                    break;
                case PreNoteConnection.Slide:
                    this.DrawTie(this.Element, "sl.", beamSlope);
                    break;
                case PreNoteConnection.SlideInFromHigher:
                    renderingContext.DrawGliss(this.Element.OwnerBeat.OwnerColumn.GetPosition(renderingContext),
                                               this.Element.String,
                                               GlissDirection.FromHigher,
                                               this.GetInstructionY(this.Element, beamSlope));
                    break;
                case PreNoteConnection.SlideInFromLower:
                    renderingContext.DrawGliss(this.Element.OwnerBeat.OwnerColumn.GetPosition(renderingContext),
                                               this.Element.String,
                                               GlissDirection.FromLower,
                                               this.GetInstructionY(this.Element, beamSlope));
                    break;
                case PreNoteConnection.Hammer:
                    this.DrawTie(this.Element, "h.", beamSlope);
                    break;
                case PreNoteConnection.Pull:
                    this.DrawTie(this.Element, "p.", beamSlope);
                    break;
            }
        }

    }
}
