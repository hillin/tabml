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
            drawingContext.DrawFretNumber(this.Note.String, this.Note.Fret.ToString(), beat.Column.Position,
                                          beat.Column.GetNoteHeadOffset(this.Note.String),
                                          beat.Beat.NoteValue.Base >= BaseNoteValue.Half);

            if (beat == this.OwnerBeat) // only draw connections if we are drawing for ourselves
                this.DrawPreConnection(drawingContext, beamSlope);
        }

        private void DrawTie(IBarDrawingContext drawingContext, string instruction, BeamSlope beamSlope)
        {
            if (this.PreConnectedNote != null) // todo: handle cross-bar ties
            {
                var instructionY = 0.0;
                if (!string.IsNullOrEmpty(instruction))
                {
                    var instructionX = (this.OwnerBeat.Column.Position + this.PreConnectedNote.OwnerBeat.Column.Position) / 2;
                    instructionY = beamSlope.GetY(instructionX);
                }

                drawingContext.DrawTie(this.PreConnectedNote.OwnerBeat.Column.Position,
                                       this.OwnerBeat.Column.Position, this.Note.String,
                                       this.OwnerBeat.VoicePart, instruction, instructionY);
            }
        }

        private void DrawPreConnection(IBarDrawingContext drawingContext, BeamSlope beamSlope)
        {


            switch (this.Note.PreConnection)
            {
                case PreNoteConnection.Tie:
                    if (this.PreConnectedNote != null) // todo: handle cross-bar ties
                    {
                        this.DrawTie(drawingContext, null, beamSlope);
                        drawingContext.DrawTie(this.PreConnectedNote.OwnerBeat.Column.Position,
                                               this.OwnerBeat.Column.Position, this.Note.String,
                                               this.OwnerBeat.VoicePart, null, 0);
                    }
                    break;
                case PreNoteConnection.Slide:
                    if (this.PreConnectedNote != null) // todo: handle cross-bar slides
                    {
                        this.DrawTie(drawingContext, "sl.", beamSlope);
                    }
                    break;
                case PreNoteConnection.SlideInFromHigher:
                    //todo
                    break;
                case PreNoteConnection.SlideInFromLower:
                    //todo
                    break;
                case PreNoteConnection.Hammer:
                    if (this.PreConnectedNote != null) // todo: handle cross-bar hammers
                    {
                        this.DrawTie(drawingContext, "h.", beamSlope);
                    }
                    break;
                case PreNoteConnection.Pull:
                    if (this.PreConnectedNote != null) // todo: handle cross-bar pulls
                    {
                        this.DrawTie(drawingContext, "p.", beamSlope);
                    }
                    break;
            }
        }
    }
}
