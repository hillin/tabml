using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Core.String;
using TabML.Core.Style;

namespace TabML.Editor.Rendering
{
    static class NoteConnectionRenderer
    {
        public static async Task DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, int stringIndex, Core.Style.VerticalDirection tiePosition)
        {
            await NoteConnectionRenderer.DrawTie(rootRenderer, from, to, new[] { stringIndex }, tiePosition);
        }

        public static async Task<Rect> DrawTie(IRootElementRenderer rootRenderer, Beat from, Beat to, IEnumerable<int> stringIndices, Core.Style.VerticalDirection tiePosition)
        {
            if (to.IsRest || from.IsRest)
                return default(Rect);

            Debug.Assert(from.VoicePart == to.VoicePart);

            var toContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(to).RenderingContext;
            var fromContext = rootRenderer.GetRenderer<Beat, BeatRenderer>(from).RenderingContext;

            var bounds = new BoundingBoxUnion();

            foreach (var stringIndex in stringIndices)
            {
                var currentBounds = toContext.GetNoteBoundingBox(to.OwnerColumn.ColumnIndex, stringIndex);
                var previousBounds = fromContext.GetNoteBoundingBox(from.OwnerColumn.ColumnIndex, stringIndex); 
                Debug.Assert(currentBounds != null, "currentBounds != null");
                Debug.Assert(previousBounds != null, "previousBounds != null");

                var fromX = previousBounds.Value.Right + toContext.Style.NoteMargin;
                var toX = currentBounds.Value.Left - toContext.Style.NoteMargin;

                if (toContext.Owner == fromContext.Owner)   // both in same row
                {
                    bounds.AddBounds(await toContext.Owner.DrawTie(fromX, toX, stringIndex, tiePosition, null, 0));
                }
                else
                {
                    bounds.AddBounds(await toContext.Owner.DrawTie(toContext.Owner.Location.X + toContext.Owner.HeaderWidth,
                                                                   toX, stringIndex, tiePosition, null, 0));

                    await fromContext.Owner.DrawTie(fromX,
                                                    fromContext.Owner.BottomRight.X,
                                                    stringIndex, tiePosition, null, 0);

                }
            }

            Debug.Assert(bounds.HasAnyBounds);
            return bounds.Bounds;
        }

        public static async Task<Rect> DrawGliss(IRootElementRenderer rootRenderer, Beat beat,
                                                 IEnumerable<int> stringIndices,
                                                 GlissDirection direction)
        {
            var beatRenderer = rootRenderer.GetRenderer<Beat, BeatRenderer>(beat);
            var renderingContext = beatRenderer.RenderingContext;

            var bounds = new BoundingBoxUnion();

            foreach (var stringIndex in stringIndices)
            {
                var noteBounds = renderingContext.GetNoteBoundingBox(beat.OwnerColumn.ColumnIndex, stringIndex);
                Debug.Assert(noteBounds != null);

                double x;
                switch (direction)
                {
                    case GlissDirection.FromHigher:
                    case GlissDirection.FromLower:
                        x = noteBounds.Value.Left - renderingContext.Style.NoteMargin;
                        break;
                    case GlissDirection.ToHigher:
                    case GlissDirection.ToLower:
                        x = noteBounds.Value.Right + renderingContext.Style.NoteMargin;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                bounds.AddBounds(await renderingContext.DrawGliss(x, stringIndex, direction, beat.VoicePart));
            }

            Debug.Assert(bounds.HasAnyBounds);
            return bounds.Bounds;
        }

        public static Task<Rect> DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from,
                                             Beat to, int stringIndex, Core.Style.VerticalDirection tiePosition)
        {
            return NoteConnectionRenderer.DrawConnection(rootRenderer, connection, from, to, new[] { stringIndex }, tiePosition);
        }

        public static async Task<Rect> DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from, Beat to, IEnumerable<int> stringIndices, Core.Style.VerticalDirection tiePosition)
        {
            switch (connection)
            {
                case NoteConnection.Slide:
                case NoteConnection.Hammer:
                case NoteConnection.Pull:
                    return await NoteConnectionRenderer.DrawTie(rootRenderer, @from, to, stringIndices, tiePosition);
                case NoteConnection.SlideInFromHigher:
                case NoteConnection.SlideInFromLower:
                    return await NoteConnectionRenderer.DrawGliss(rootRenderer, to, stringIndices, (GlissDirection)connection);
                    case NoteConnection.SlideOutToHigher:
                case NoteConnection.SlideOutToLower:
                    return await NoteConnectionRenderer.DrawGliss(rootRenderer, from, stringIndices, (GlissDirection)connection);
            }

            return default(Rect);
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
