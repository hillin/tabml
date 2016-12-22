using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    static class NoteConnectionRenderer
    {
        public static void DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, int stringIndex, TiePosition tiePosition)
        {
            NoteConnectionRenderer.DrawTie(rootRenderer, from, to, new[] { stringIndex }, tiePosition);
        }

        private static void DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, IEnumerable<int> stringIndices, TiePosition tiePosition, string instruction = null)
        {
            if (!to.IsRest && !from.IsRest)
            {
                Debug.Assert(from.VoicePart == to.VoicePart);

                var toRenderingContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(to).RenderingContext;
                var tieTo = to.OwnerColumn.GetPositionInRow(toRenderingContext);

                var fromRenderingContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(from).RenderingContext;
                var tieFrom = @from.OwnerColumn.GetPositionInRow(fromRenderingContext);

                if (toRenderingContext.Owner == fromRenderingContext.Owner)
                {
                    foreach (var stringIndex in stringIndices)
                    {
                        var beatAlternationOffset = to.GetAlternationOffset(toRenderingContext, stringIndex);
                        var previousBeatAlternationOffset = @from.GetAlternationOffset(fromRenderingContext, stringIndex);
                        toRenderingContext.Owner.DrawTie(tieFrom + beatAlternationOffset,
                                                              tieTo + previousBeatAlternationOffset,
                                                              stringIndex,
                                                              tiePosition, instruction, 0);  //todo: fill instruction Y
                    }
                }
                else
                {
                    foreach (var stringIndex in stringIndices)
                    {
                        var beatAlternationOffset = to.GetAlternationOffset(toRenderingContext, stringIndex);
                        var previousBeatAlternationOffset = @from.GetAlternationOffset(fromRenderingContext, stringIndex);
                        toRenderingContext.Owner.DrawTie(0, tieTo + previousBeatAlternationOffset,
                                                              stringIndex,
                                                              tiePosition, instruction, 0);  //todo: fill instruction Y
                        fromRenderingContext.Owner.DrawTie(tieFrom + beatAlternationOffset,
                                                               fromRenderingContext.Owner.AvailableSize.Width,
                                                               stringIndex,
                                                               tiePosition, instruction, 0);  //todo: fill instruction Y
                    }
                }
            }
        }

        public static void DrawGliss(IRootElementRenderer rootRenderer, Beat beat, IEnumerable<int> stringIndices,
                                     GlissDirection direction)
        {
            var renderingContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(beat).RenderingContext;

            foreach (var stringIndex in stringIndices)
            {
                renderingContext.DrawGliss(beat.OwnerColumn.GetPosition(renderingContext),
                                           stringIndex,
                                           direction);
            }
        }

        public static void DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from,
                                             Beat to, int stringIndex, TiePosition tiePosition)
        {
            NoteConnectionRenderer.DrawConnection(rootRenderer, connection, from, to, new[] {stringIndex}, tiePosition);
        }

        public static void DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from, Beat to, IEnumerable<int> stringIndices, TiePosition tiePosition)
        {
            switch (connection)
            {
                case NoteConnection.Slide:
                    NoteConnectionRenderer.DrawTie(rootRenderer, from, to, stringIndices, tiePosition, "sl.");
                    break;
                case NoteConnection.SlideInFromHigher:
                    NoteConnectionRenderer.DrawGliss(rootRenderer, to, stringIndices, GlissDirection.FromHigher);
                    break;
                case NoteConnection.SlideInFromLower:
                    NoteConnectionRenderer.DrawGliss(rootRenderer, to, stringIndices, GlissDirection.FromLower);
                    break;
                case NoteConnection.SlideOutToHigher:
                    NoteConnectionRenderer.DrawGliss(rootRenderer, from, stringIndices, GlissDirection.ToHigher);
                    break;
                case NoteConnection.SlideOutToLower:
                    NoteConnectionRenderer.DrawGliss(rootRenderer, from, stringIndices, GlissDirection.ToLower);
                    break;
                case NoteConnection.Hammer:
                    NoteConnectionRenderer.DrawTie(rootRenderer, from, to, stringIndices, tiePosition, "h.");
                    break;
                case NoteConnection.Pull:
                    NoteConnectionRenderer.DrawTie(rootRenderer, from, to, stringIndices, tiePosition, "p.");
                    break;
            }
        }

        public static void DrawPostConnection(IRootElementRenderer rootRenderer, PostNoteConnection postConnection,
                                              Beat beat, int stringIndex)
        {
            NoteConnectionRenderer.DrawPostConnection(rootRenderer, postConnection, beat, new[] { stringIndex });
        }

        public static void DrawPostConnection(IRootElementRenderer rootRenderer, PostNoteConnection postConnection,
                                              Beat beat, IEnumerable<int> stringIndices)
        {
            switch (postConnection)
            {
                case PostNoteConnection.SlideOutToHigher:
                    NoteConnectionRenderer.DrawGliss(rootRenderer, beat, stringIndices, GlissDirection.ToHigher);
                    break;
                case PostNoteConnection.SlideOutToLower:
                    NoteConnectionRenderer.DrawGliss(rootRenderer, beat, stringIndices, GlissDirection.ToLower);
                    break;
            }
        }
    }
}
