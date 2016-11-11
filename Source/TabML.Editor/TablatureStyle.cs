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
        public int StringCount { get; set; } = 6;
        public BeatLayout BeatLayout { get; set; } = BeatLayout.SizeByNoteValue;
        public double MinimumBeatSize { get; set; } = 24;
        public double BarLineHeight { get; set; } = 12;
        public double BarTopMargin { get; set; } = 120;
        public double BarBottomMargin { get; set; } = 60;
        public double BarHorizontalPadding { get; set; } = 32;

        public Thickness Padding { get; set; } = new Thickness(24);

        public bool FlexibleBeatSize { get; set; } = false;
        public Typeface LyricsTypeface { get; set; } = new Typeface("Segoe UI");
        public double LyricsFontSize { get; set; } = 12;
        public Brush LyricsForeground { get; set; } = Brushes.Black;

        public double NoteStemOffset { get; set; } = 2;
        public double NoteTailOffset { get; set; } = 36;

        public double BeamThickness { get; set; } = 4;
        public double BeamSpacing { get; set; } = 4;
        public double HalfBeamWidth { get; set; } = 12;
        public double NoteValueAugmentOffset { get; set; } = 4;

        public FormattedText MakeFormattedLyrics(string lyricsText)
        {
            return new FormattedText(lyricsText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     this.LyricsTypeface, this.LyricsFontSize, this.LyricsForeground);
        }
    }
}
