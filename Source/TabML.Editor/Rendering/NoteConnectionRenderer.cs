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
        public static async Task DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, int stringIndex, TiePosition tiePosition)
        {
            await NoteConnectionRenderer.DrawTie(rootRenderer, from, to, new[] { stringIndex }, tiePosition);
        }

        public static async Task DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, IEnumerable<int> stringIndices, TiePosition tiePosition, string instruction = null)
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
                    if (!string.IsNullOrEmpty(instruction))
                        await toContext.DrawTieInstruction(to.VoicePart, (toX + fromX)/2, instruction);
                }
                else
                {
                    toContext.Owner.DrawTie(toContext.Owner.Location.X, toX, stringIndex, tiePosition, null, 0);
                    fromContext.Owner.DrawTie(fromX,
                                              fromContext.Owner.BottomRight.X,
                                              stringIndex, tiePosition, null, 0);

                    if (!string.IsNullOrEmpty(instruction))
                    {
                        await toContext.DrawTieInstruction(to.VoicePart, (toX + toContext.Owner.Location.X) / 2, $"({instruction})");
                        await fromContext.DrawTieInstruction(to.VoicePart, (fromX + fromContext.Owner.BottomRight.X) / 2, instruction);
                    }
                }
            }
        }

        public static async Task DrawGliss(IRootElementRenderer rootRenderer, Beat beat, IEnumerable<int> stringIndices,
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

                await renderingContext.DrawGliss(x, stringIndex, direction, beat.VoicePart);
            }
        }

        public static async Task DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from,
                                             Beat to, int stringIndex, TiePosition tiePosition)
        {
            await NoteConnectionRenderer.DrawConnection(rootRenderer, connection, from, to, new[] { stringIndex }, tiePosition);
        }

        public static async Task DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from, Beat to, IEnumerable<int> stringIndices, TiePosition tiePosition)
        {
            switch (connection)
            {
                case NoteConnection.Slide:
                    NoteConnectionRenderer.DrawTie(rootRenderer, from, to, stringIndices, tiePosition, "sl.");
                    break;
                case NoteConnection.SlideInFromHigher:
                    await NoteConnectionRenderer.DrawGliss(rootRenderer, to, stringIndices, GlissDirection.FromHigher);
                    break;
                case NoteConnection.SlideInFromLower:
                    await NoteConnectionRenderer.DrawGliss(rootRenderer, to, stringIndices, GlissDirection.FromLower);
                    break;
                case NoteConnection.SlideOutToHigher:
                    await NoteConnectionRenderer.DrawGliss(rootRenderer, from, stringIndices, GlissDirection.ToHigher);
                    break;
                case NoteConnection.SlideOutToLower:
                    await NoteConnectionRenderer.DrawGliss(rootRenderer, from, stringIndices, GlissDirection.ToLower);
                    break;
                case NoteConnection.Hammer:
                    NoteConnectionRenderer.DrawTie(rootRenderer, from, to, stringIndices, tiePosition, "h.");
                    break;
                case NoteConnection.Pull:
                    NoteConnectionRenderer.DrawTie(rootRenderer, from, to, stringIndices, tiePosition, "p.");
                    break;
            }
        }

        public static async Task DrawPostConnection(IRootElementRenderer rootRenderer, PostNoteConnection postConnection,
                                              Beat beat, int stringIndex)
        {
            await NoteConnectionRenderer.DrawPostConnection(rootRenderer, postConnection, beat, new[] { stringIndex });
        }

        public static async Task DrawPostConnection(IRootElementRenderer rootRenderer, PostNoteConnection postConnection,
                                              Beat beat, IEnumerable<int> stringIndices)
        {
            switch (postConnection)
            {
                case PostNoteConnection.SlideOutToHigher:
                    await NoteConnectionRenderer.DrawGliss(rootRenderer, beat, stringIndices, GlissDirection.ToHigher);
                    break;
                case PostNoteConnection.SlideOutToLower:
                    await NoteConnectionRenderer.DrawGliss(rootRenderer, beat, stringIndices, GlissDirection.ToLower);
                    break;
            }
        }
    }
}
