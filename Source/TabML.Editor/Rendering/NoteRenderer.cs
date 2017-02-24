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
    class NoteRenderer : ElementRenderer<BeatNote, BeatRenderingContext>
    {
        public BeatRenderer OwnerBeat { get; set; }

        private bool IsTied => this.OwnerBeat.Element.IsTied || this.Element.IsTied;

        public NoteRenderer(BeatRenderer owner, BeatNote element) : base(owner, element)
        {
            this.OwnerBeat = owner;
        }

        /// <remarks>
        /// <para>beat</para> could be different than <c>this.OwnerBeat</c> because we may draw for tied beats
        /// </remarks>
        public async Task Render(BeamSlope beamSlope)
        {
            var x = this.OwnerBeat.GetNoteRenderingPosition(this.Element.String);

            var flags = this.GetNoteRenderingFlags();

            Rect bounds;
            if (this.Element.EffectTechnique == NoteEffectTechnique.DeadNote)
            {
                bounds = await this.RenderingContext.Owner.DrawNoteFretting(this.Element.String, "dead", x, flags);
            }
            else if (this.Element.Fret == BeatNote.UnspecifiedFret)
            {
                bounds = await this.RenderingContext.Owner.DrawNoteFretting(this.Element.String, "asChord", x, flags);
            }
            else
            {
                bounds =
                    await
                        this.RenderingContext.Owner.DrawNoteFretting(this.Element.String, this.Element.Fret.ToString(),
                                                                     x, flags);
            }

            if (!this.IsTied && this.Element.EffectTechnique == NoteEffectTechnique.ArtificialHarmonic)
            {
                this.RenderingContext.AddArtificialHarmonic(this.Element.String,
                                                            this.Element.Fret,
                                                            this.Element.EffectTechniqueParameter == null
                                                                ? this.Element.Fret + 12
                                                                : (int)this.Element.EffectTechniqueParameter);
            }

            this.RenderingContext.Owner.SetNoteBoundingBox(this.OwnerBeat.Element.OwnerColumn.ColumnIndex, this.Element.String,
                                                           bounds);

            if (this.OwnerBeat.Element.NoteValue.Augment != NoteValueAugment.None)
            {
                var spaceOffset = this.OwnerBeat.Element.GetStemRenderVoicePart() == VoicePart.Treble ? 0 : 1;
                this.RenderingContext.Owner.DrawNoteValueAugment(this.OwnerBeat.Element.NoteValue,
                                                                 bounds.Right - this.RenderingContext.Owner.Location.X,
                                                                 this.Element.String + spaceOffset);
            }

            if (this.IsTied)
            {
                var tiePosition = this.Element.TiePosition ?? this.OwnerBeat.Element.GetRenderTiePosition();

                if (this.OwnerBeat.Element.IsTied)
                {
                    await NoteConnectionRenderer.DrawTie(this.Root, this.OwnerBeat.Element.PreviousBeat,
                                                         this.OwnerBeat.Element, this.Element.String, tiePosition);
                }
                else
                {
                    Debug.Assert(this.OwnerBeat.Element == this.Element.OwnerBeat);

                    if (this.Element.IsTied)
                    {
                        await NoteConnectionRenderer.DrawTie(this.Root, this.Element.PreConnectedNote?.OwnerBeat,
                                                             this.OwnerBeat.Element, this.Element.String, tiePosition);

                    }
                    else
                    {
                        var preConnection = this.Element.PreConnection == PreNoteConnection.None
                            ? (NoteConnection)this.Element.OwnerBeat.PreConnection
                            : (NoteConnection)this.Element.PreConnection;

                        if (preConnection != NoteConnection.None)
                        {
                            await this.RenderingContext.DrawConnection(this.Root, preConnection,
                                                                       this.Element.PreConnectedNote?.OwnerBeat,
                                                                       this.OwnerBeat.Element, this.Element.String,
                                                                       tiePosition);
                        }
                    }

                    var postConnection = this.Element.PostConnection == PostNoteConnection.None
                        ? (NoteConnection)this.Element.OwnerBeat.PostConnection
                        : (NoteConnection)this.Element.PostConnection;

                    if (postConnection != NoteConnection.None)
                        await this.RenderingContext.DrawConnection(this.Root, postConnection, this.OwnerBeat.Element, null, this.Element.String, tiePosition);
                }
            }

            
        }

        private NoteRenderingFlags GetNoteRenderingFlags()
        {
            var flags = NoteRenderingFlags.None;

            if (!this.IsTied)
            {
                if (this.Element.Accent == NoteAccent.Ghost)
                    flags |= NoteRenderingFlags.Ghost;

                if (this.Element.EffectTechnique == NoteEffectTechnique.ArtificialHarmonic)
                    flags |= NoteRenderingFlags.ArtificialHarmonic;

                if (this.Element.EffectTechnique == NoteEffectTechnique.NaturalHarmonic)
                    flags |= NoteRenderingFlags.NaturalHarmonic;
            }

            return flags;
        }
    }
}
