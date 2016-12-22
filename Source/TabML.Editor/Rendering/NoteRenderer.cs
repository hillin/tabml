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
                // todo: handle tie chain
                var tiePosition = this.Element.TiePosition ?? this.Element.OwnerBeat.VoicePart.GetDefaultTiePosition();

                if (this.Element.IsTied)
                {
                    NoteConnectionRenderer.DrawTie(this.Root, this.Element.PreConnectedNote?.OwnerBeat,
                                                   this.Element.OwnerBeat, this.Element.String, tiePosition);
                }
                else
                {
                    var preConnection = this.Element.PreConnection == PreNoteConnection.None
                        ? (NoteConnection) this.Element.OwnerBeat.PreConnection
                        : (NoteConnection) this.Element.PreConnection;

                    if (preConnection != NoteConnection.None)
                    {
                        NoteConnectionRenderer.DrawConnection(this.Root, preConnection,
                                                              this.Element.PreConnectedNote?.OwnerBeat,
                                                              this.Element.OwnerBeat, this.Element.String,
                                                              this.Element.TiePosition ??
                                                              tiePosition);
                    }
                }

                var postConnection = this.Element.PostConnection == PostNoteConnection.None
                    ? (NoteConnection)this.Element.OwnerBeat.PostConnection
                    : (NoteConnection)this.Element.PostConnection;

                if (postConnection != NoteConnection.None)
                    NoteConnectionRenderer.DrawConnection(this.Root, postConnection, beat, null, this.Element.String, tiePosition);
            }
        }

    }
}
