using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedNote
    {
        public BeatNote Note { get; }
        public ArrangedBeat OwnerBeat { get; }
        public ArrangedNote PreConnectedNote { get; set; }

        public ArrangedNote(BeatNote note, ArrangedBeat ownerBeat)
        {
            this.Note = note;
            this.OwnerBeat = ownerBeat;
        }

        /// <remarks>
        /// <para>beat</para> could be different than <c>this.OwnerBeat</c> because we may draw for tied beats
        /// </remarks>
        public void Draw(IBarDrawingContext drawingContext, ArrangedBeat beat, BeamSlope beamSlope)
        {
            if (this.Note.EffectTechnique == NoteEffectTechnique.DeadNote)
            {
                drawingContext.DrawDeadNote(this.Note.String, beat.Column.Position,
                                            beat.Column.GetNoteHeadOffset(this.Note.String),
                                            beat.Beat.NoteValue.Base >= BaseNoteValue.Half);
            }
            else
            {
                drawingContext.DrawFretNumber(this.Note.String, this.Note.Fret.ToString(), beat.Column.Position,
                                              beat.Column.GetNoteHeadOffset(this.Note.String),
                                              beat.Beat.NoteValue.Base >= BaseNoteValue.Half);
            }

            if (beat == this.OwnerBeat) // only draw connections if we are drawing for ourselves
            {
                this.DrawPreConnection(drawingContext, beamSlope);
                this.DrawPostConnection(drawingContext, beamSlope);
            }
        }

        private void DrawPostConnection(IBarDrawingContext drawingContext, BeamSlope beamSlope)
        {
            switch (this.Note.PostConnection)
            {
                case PostNoteConnection.SlideOutToHigher:
                    drawingContext.DrawGliss(this.OwnerBeat.Column.Position, this.Note.String, GlissDirection.ToHigher, this.GetInstructionY(drawingContext, beamSlope));
                    break;
                case PostNoteConnection.SlideOutToLower:
                    drawingContext.DrawGliss(this.OwnerBeat.Column.Position, this.Note.String, GlissDirection.ToLower, this.GetInstructionY(drawingContext, beamSlope));
                    break;
            }
        }

        private void DrawTie(IBarDrawingContext drawingContext, string instruction, BeamSlope beamSlope)
        {
            if (this.PreConnectedNote != null) // todo: handle cross-bar ties
            {
                var instructionY = string.IsNullOrEmpty(instruction) ? 0.0 : this.GetInstructionY(drawingContext, beamSlope);

                drawingContext.DrawTie(this.PreConnectedNote.OwnerBeat.Column.Position,
                                       this.OwnerBeat.Column.Position, this.Note.String,
                                       this.OwnerBeat.VoicePart, instruction, instructionY);
            }
        }

        private double GetInstructionY(IBarDrawingContext drawingContext, BeamSlope beamSlope)
        {
            if (beamSlope == null)
            {
                return this.OwnerBeat.GetStemTailPosition(drawingContext);
            }
            else
            {
                var instructionX = this.PreConnectedNote != null
                    ? (this.OwnerBeat.Column.Position + this.PreConnectedNote.OwnerBeat.Column.Position)/2
                    : this.OwnerBeat.Column.Position;
                return beamSlope.GetY(instructionX);
            }
        }

        private void DrawPreConnection(IBarDrawingContext drawingContext, BeamSlope beamSlope)
        {
            switch (this.Note.PreConnection)
            {
                case PreNoteConnection.Tie:
                        this.DrawTie(drawingContext, null, beamSlope);
                        drawingContext.DrawTie(this.PreConnectedNote.OwnerBeat.Column.Position,
                                               this.OwnerBeat.Column.Position, this.Note.String,
                                               this.OwnerBeat.VoicePart, null, 0);
                    break;
                case PreNoteConnection.Slide:
                        this.DrawTie(drawingContext, "sl.", beamSlope);
                    break;
                case PreNoteConnection.SlideInFromHigher:
                    drawingContext.DrawGliss(this.OwnerBeat.Column.Position, this.Note.String, GlissDirection.FromHigher, this.GetInstructionY(drawingContext, beamSlope));
                    break;
                case PreNoteConnection.SlideInFromLower:
                    drawingContext.DrawGliss(this.OwnerBeat.Column.Position, this.Note.String, GlissDirection.FromLower, this.GetInstructionY(drawingContext, beamSlope));
                    break;
                case PreNoteConnection.Hammer:
                        this.DrawTie(drawingContext, "h.", beamSlope);
                    break;
                case PreNoteConnection.Pull:
                        this.DrawTie(drawingContext, "p.", beamSlope);
                    break;
            }
        }
    }
}
