using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BeatNoteExtensions
    {
        public static BeatNote CloneAsTied(this BeatNote note)
        {
            var clone = note.Clone();
            clone.IsTied = true;
            return clone;
        }
    }
}
