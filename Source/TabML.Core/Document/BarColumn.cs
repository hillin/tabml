using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class BarColumn
    {
        public int ColumnIndex { get; }
        public List<Beat> VoiceBeats { get; }
        public Chord Chord { get; set; }
        public LyricsSegment Lyrics { get; set; }

        public BarColumn(int columnIndex)
        {
            this.ColumnIndex = columnIndex;
            this.VoiceBeats = new List<Beat>();
        }

        public PreciseDuration GetDuration() => this.VoiceBeats.Min(v => v.GetDuration());
    }
}
