using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class BarColumn : VirtualElement, IBarElement
    {
        public Bar OwnerBar { get; }
        public int ColumnIndex { get; }
        public List<Beat> VoiceBeats { get; }
        public Chord Chord { get; set; }
        public LyricsSegment Lyrics { get; set; }
        public bool IsFirstColumnOfSegment { get; set; }

        public BarColumn(Bar ownerBar, int columnIndex)
        {
            this.OwnerBar = ownerBar;
            this.ColumnIndex = columnIndex;
            this.VoiceBeats = new List<Beat>();
        }

        public PreciseDuration GetDuration() => this.VoiceBeats.Min(v => v.GetDuration());

    }
}
