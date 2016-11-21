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
    class NoteRenderer
    {
        /// <remarks>
        /// <para>beat</para> could be different than <c>this.OwnerBeat</c> because we may draw for tied beats
        /// </remarks>
        public void Render(BarDrawingContext drawingContext, BeatNote note, Beat beat, BeamSlope beamSlope)
        {
            var alternationOffsetRatio =
                drawingContext.ColumnRenderingInfos[beat.OwnerColumn.ColumnIndex].GetNoteAlternationOffsetRatio(note.String);

            var x = beat.OwnerColumn.GetPosition(drawingContext) +
                    drawingContext.GetNoteAlternationOffset(alternationOffsetRatio);

            var isHalfOrLonger = beat.NoteValue.Base >= BaseNoteValue.Half;
            if (note.EffectTechnique == NoteEffectTechnique.DeadNote)
            {
                drawingContext.DrawDeadNote(note.String, x, isHalfOrLonger);
            }
            else if (note.Fret == BeatNote.UnspecifiedFret)
            {
                drawingContext.DrawPlayAsChordMark(note.String, x, isHalfOrLonger);
            }
            else
            {
                drawingContext.DrawFretNumber(note.String, note.Fret.ToString(), x, isHalfOrLonger);
            }

            if (beat == note.OwnerBeat) // only draw connections if we are drawing for ourselves
            {
                this.DrawPreConnection(drawingContext, note, beamSlope);
                this.DrawPostConnection(drawingContext, note, beamSlope);
            }
        }

        private void DrawPostConnection(BarDrawingContext drawingContext, BeatNote note, BeamSlope beamSlope)
        {
            switch (note.PostConnection)
            {
                case PostNoteConnection.SlideOutToHigher:
                    drawingContext.DrawGliss(note.OwnerBeat.OwnerColumn.GetPosition(drawingContext), note.String,
                                             GlissDirection.ToHigher,
                                             this.GetInstructionY(drawingContext, note, beamSlope));
                    break;
                case PostNoteConnection.SlideOutToLower:
                    drawingContext.DrawGliss(note.OwnerBeat.OwnerColumn.GetPosition(drawingContext), note.String,
                                             GlissDirection.ToLower,
                                             this.GetInstructionY(drawingContext, note, beamSlope));
                    break;
            }
        }

        private void DrawTie(BarDrawingContext drawingContext, BeatNote note, string instruction, BeamSlope beamSlope)
        {
            if (note.PreConnectedNote != null) // todo: handle cross-bar ties
            {
                var instructionY = string.IsNullOrEmpty(instruction)
                    ? 0.0
                    : this.GetInstructionY(drawingContext, note, beamSlope);

                drawingContext.DrawTie(note.PreConnectedNote.OwnerBeat.OwnerColumn.GetPosition(drawingContext),
                                       note.OwnerBeat.OwnerColumn.GetPosition(drawingContext), note.String,
                                       note.OwnerBeat.VoicePart, instruction, instructionY);
            }
        }

        private double GetInstructionY(BarDrawingContext drawingContext, BeatNote note, BeamSlope beamSlope)
        {
            if (beamSlope == null)
            {
                return note.OwnerBeat.GetStemTailPosition(drawingContext);
            }
            else
            {
                var instructionX = note.PreConnectedNote != null
                    ? (note.OwnerBeat.OwnerColumn.GetPosition(drawingContext) +
                       note.PreConnectedNote.OwnerBeat.OwnerColumn.GetPosition(drawingContext))/2
                    : note.OwnerBeat.OwnerColumn.GetPosition(drawingContext);
                return beamSlope.GetY(instructionX);
            }
        }

        private void DrawPreConnection(BarDrawingContext drawingContext, BeatNote note, BeamSlope beamSlope)
        {
            switch (note.PreConnection)
            {
                case PreNoteConnection.Tie:
                    this.DrawTie(drawingContext, note, null, beamSlope);
                    drawingContext.DrawTie(note.PreConnectedNote.OwnerBeat.OwnerColumn.GetPosition(drawingContext),
                                           note.OwnerBeat.OwnerColumn.GetPosition(drawingContext), note.String,
                                           note.OwnerBeat.VoicePart, null, 0);
                    break;
                case PreNoteConnection.Slide:
                    this.DrawTie(drawingContext, note, "sl.", beamSlope);
                    break;
                case PreNoteConnection.SlideInFromHigher:
                    drawingContext.DrawGliss(note.OwnerBeat.OwnerColumn.GetPosition(drawingContext), note.String,
                                             GlissDirection.FromHigher,
                                             this.GetInstructionY(drawingContext, note, beamSlope));
                    break;
                case PreNoteConnection.SlideInFromLower:
                    drawingContext.DrawGliss(note.OwnerBeat.OwnerColumn.GetPosition(drawingContext), note.String,
                                             GlissDirection.FromLower,
                                             this.GetInstructionY(drawingContext, note, beamSlope));
                    break;
                case PreNoteConnection.Hammer:
                    this.DrawTie(drawingContext, note, "h.", beamSlope);
                    break;
                case PreNoteConnection.Pull:
                    this.DrawTie(drawingContext, note, "p.", beamSlope);
                    break;
            }
        }
    }
}
