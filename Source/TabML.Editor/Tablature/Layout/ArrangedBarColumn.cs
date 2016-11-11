using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using Chord = TabML.Core.Document.Chord;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBarColumn
    {
        public int ColumnIndex { get; }
        public List<ArrangedBarBeat> VoiceBeats { get; }
        public Chord Chord { get; set; }
        public LyricsSegment Lyrics { get; set; }

        private bool[] _occupiedStrings; // string indices occupied by this column, in ascending order

        public ArrangedBarColumn(int columnIndex)
        {
            this.ColumnIndex = columnIndex;
            this.VoiceBeats = new List<ArrangedBarBeat>();
        }

        public PreciseDuration GetDuration() => this.VoiceBeats.Min(v => v.GetDuration());

        public double GetMinWidth(TablatureStyle style)
        {
            return Math.Max(style.MinimumBeatSize,
                            this.Lyrics == null ? 0.0 : style.MakeFormattedLyrics(this.Lyrics.Text).Width);
        }

        public void Draw(IBarDrawingContext drawingContext, double position, double width)
        {
            foreach (var beat in this.VoiceBeats)
                beat.DrawHead(drawingContext, position, width);

            //todo: draw chord and lyrics
        }

        public double GetNoteHeadOffset(int stringIndex)
        {
            if (_occupiedStrings == null)
            {
                _occupiedStrings = new bool[Defaults.Strings];
                foreach (var i in this.VoiceBeats.SelectMany(b => b.Beat.Notes.Select(n => n.String - 1)))
                    _occupiedStrings[i] = true;
            }

            if (stringIndex == 0)
                return _occupiedStrings[1] ? -0.25 : 0;

            var continuousStringsBefore = 0;
            for (var i = stringIndex - 1; i >= 0; --i)
            {
                if (_occupiedStrings[i])
                    ++continuousStringsBefore;
                else
                    break;
            }

            if (continuousStringsBefore == 0)
            {
                if (stringIndex == Defaults.Strings - 1)
                    return 0;

                if (_occupiedStrings[stringIndex + 1])
                    return -0.25;

                return 0;
            }

            return (continuousStringsBefore % 2 - 0.5) / 2;
        }
    }
}
