﻿using System;
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
    class NoteRenderer : ElementRenderer<BeatNote, BarRenderingContext>
    {
        public NoteRenderer(ElementRenderer owner, BeatNote element) : base(owner, element)
        {
        }

        /// <remarks>
        /// <para>beat</para> could be different than <c>this.OwnerBeat</c> because we may draw for tied beats
        /// </remarks>
        public async Task Render(Beat beat, BeamSlope beamSlope)
        {
            var renderingContext = this.Root.GetRenderer<Beat, BeatRenderer>(beat).RenderingContext;

            var x = this.Element.GetRenderPosition(renderingContext, beat);

            var isHalfOrLonger = beat.NoteValue.Base >= BaseNoteValue.Half;

            Rect bounds;
            if (this.Element.EffectTechnique == NoteEffectTechnique.DeadNote)
            {
                bounds = await renderingContext.DrawDeadNote(this.Element.String, x, isHalfOrLonger);
            }
            else if (this.Element.Fret == BeatNote.UnspecifiedFret)
            {
                bounds = await renderingContext.DrawPlayAsChordMark(this.Element.String, x, isHalfOrLonger);
            }
            else
            {
                bounds = await renderingContext.DrawFretNumber(this.Element.String, this.Element.Fret.ToString(), x, isHalfOrLonger);
            }

            renderingContext.SetNoteBoundingBox(beat.OwnerColumn, this.Element.String, bounds);

            if (beat == this.Element.OwnerBeat) // only draw connections if we are drawing for ourselves
            {
                // todo: handle tie chain
                var tiePosition = this.Element.TiePosition ?? this.Element.OwnerBeat.VoicePart.GetDefaultTiePosition();

                if (this.Element.IsTied)
                {
                    await NoteConnectionRenderer.DrawTie(this.Root, this.Element.PreConnectedNote?.OwnerBeat,
                                                         this.Element.OwnerBeat, this.Element.String, tiePosition);
                }
                else
                {
                    var preConnection = this.Element.PreConnection == PreNoteConnection.None
                        ? (NoteConnection) this.Element.OwnerBeat.PreConnection
                        : (NoteConnection) this.Element.PreConnection;

                    if (preConnection != NoteConnection.None)
                    {
                        await NoteConnectionRenderer.DrawConnection(this.Root, preConnection,
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
                    await NoteConnectionRenderer.DrawConnection(this.Root, postConnection, beat, null, this.Element.String, tiePosition);
            }
        }

    }
}
