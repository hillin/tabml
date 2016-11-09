using System;
using System.Collections.Generic;
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
    }
}
