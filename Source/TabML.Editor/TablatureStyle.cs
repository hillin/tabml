using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TabML.Core.Document;

namespace TabML.Editor
{
    class TablatureStyle
    {
        public BeatLayout BeatLayout { get; set; } = BeatLayout.SizeByNoteValue;
        public double MinimumBeatSize { get; set; } = 24;
        public double BarLineHeight { get; set; } = 12;

        public bool FlexibleBeatSize { get; set; } = false;
        public Typeface LyricsTypeface { get; set; } = new Typeface("Segoe UI");
        public double LyricsFontSize { get; set; } = 12;
        public Brush LyricsForeground { get; set; } = Brushes.Black;

        public FormattedText MakeFormattedLyrics(string lyricsText)
        {
            return new FormattedText(lyricsText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     this.LyricsTypeface, this.LyricsFontSize, this.LyricsForeground);
        }
    }
}
