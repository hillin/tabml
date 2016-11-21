using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class NoteExtensions
    {
        public static double GetRenderPosition(this BeatNote note, BarDrawingContext drawingContext, Beat beat = null)
        {
            beat = beat ?? note.OwnerBeat;
            return beat.OwnerColumn.GetPosition(drawingContext) +
                   beat.GetAlternationOffset(drawingContext, note.String);
        }
    }
}
