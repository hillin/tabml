using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public struct RhythmUnitNote
    {
        public int String { get; }
        public PreNoteConnection PreConnection { get; }
        public PostNoteConnection PostConnection { get; }

        public RhythmUnitNote(int stringNumber, PreNoteConnection preConnection = PreNoteConnection.None, PostNoteConnection postConnection = PostNoteConnection.None)
        {
            this.String = stringNumber;
            this.PreConnection = preConnection;
            this.PostConnection = postConnection;
        }
    }
}
