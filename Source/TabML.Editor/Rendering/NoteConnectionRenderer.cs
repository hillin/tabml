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

        public static void DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, IEnumerable<int> stringIndices, TiePosition tiePosition, string instruction = null)
        {
            if (to.IsRest || from.IsRest)
                return;

            Debug.Assert(from.VoicePart == to.VoicePart);

            var toContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(to).RenderingContext;
            var fromContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(from).RenderingContext;

            foreach (var stringIndex in stringIndices)
            {
                var currentBounds = toContext.GetNoteBoundingBox(to.OwnerColumn, stringIndex);
                var previousBounds = fromContext.GetNoteBoundingBox(from.OwnerColumn, stringIndex); 
                Debug.Assert(currentBounds != null, "currentBounds != null");
                Debug.Assert(previousBounds != null, "previousBounds != null");

                var fromX = previousBounds.Value.Right + toContext.Style.NoteMargin;
                var toX = currentBounds.Value.Left - toContext.Style.NoteMargin;

                if (toContext.Owner == fromContext.Owner)
                {
                    toContext.Owner.DrawTie(fromX, toX, stringIndex, tiePosition, null, 0);
                }
                else
                {
                    toContext.Owner.DrawTie(toContext.Owner.Location.X, toX, stringIndex, tiePosition, null, 0);
                    fromContext.Owner.DrawTie(fromX,
                                              fromContext.Owner.BottomRight.X,
                                              stringIndex, tiePosition, null, 0);
                }
            }
        }

        public static void DrawGliss(IRootElementRenderer rootRenderer, Beat beat, IEnumerable<int> stringIndices,
                                     GlissDirection direction)
        {
            var renderingContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(beat).RenderingContext;

            foreach (var stringIndex in stringIndices)
            {
                var bounds = renderingContext.GetNoteBoundingBox(beat.OwnerColumn, stringIndex);
                Debug.Assert(bounds != null);

                double x;
                switch (direction)
                {
                    case GlissDirection.FromHigher:
                    case GlissDirection.FromLower:
                        x = bounds.Value.Left - renderingContext.Style.NoteMargin;
                        break;
                    case GlissDirection.ToHigher:
                    case GlissDirection.ToLower:
                        x = bounds.Value.Right + renderingContext.Style.NoteMargin;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                renderingContext.DrawGliss(x, stringIndex, direction);
            }
        }

        public static void DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from,
                                             Beat to, int stringIndex, TiePosition tiePosition)
        {
            NoteConnectionRenderer.DrawConnection(rootRenderer, connection, from, to, new[] { stringIndex }, tiePosition);
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
