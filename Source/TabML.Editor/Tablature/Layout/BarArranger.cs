using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using DocumentBar = TabML.Core.Document.Bar;

namespace TabML.Editor.Tablature.Layout
{
    class BarArranger
    {

        private readonly Dictionary<Beat, ArrangedBarBeat> _beatLookup;

        public BarArranger()
        {
            _beatLookup = new Dictionary<Beat, ArrangedBarBeat>();
        }

        public ArrangedBar Arrange(DocumentBar bar)
        {
            var arrangedBar = new ArrangedBar
            {
                Duration = bar.Rhythm.GetDuration(),
                OpenLine = bar.OpenLine,
                CloseLine = bar.CloseLine
            };

            this.CreateArrangedBeats(bar);
            this.ArrangeColumns(bar, arrangedBar);
            this.ArrangeVoices(bar, arrangedBar);
            //todo

            return arrangedBar;
        }

        private void CreateArrangedBeats(DocumentBar bar)
        {
            _beatLookup.Clear();
            foreach (var segment in bar.Rhythm.Segments)
            {
                if (segment.BassVoice != null)
                {
                    foreach (var beat in segment.BassVoice.Beats)
                        _beatLookup.Add(beat, new ArrangedBarBeat(beat, VoicePart.Bass));
                }

                if (segment.TrebleVoice != null)
                {
                    foreach (var beat in segment.TrebleVoice.Beats)
                        _beatLookup.Add(beat, new ArrangedBarBeat(beat, VoicePart.Treble));
                }
            }
        }

        private void ArrangeVoices(DocumentBar bar, ArrangedBar arrangedBar)
        {
            var bassBeatArranger = new BeatArranger(bar.DocumentState.Time.NoteValue, VoicePart.Bass);
            var trebleBeatArranger = new BeatArranger(bar.DocumentState.Time.NoteValue, VoicePart.Treble);

            foreach (var segment in bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                this.AppendAndArrangeVoice(segment.BassVoice, bassBeatArranger);
                this.AppendAndArrangeVoice(segment.TrebleVoice, trebleBeatArranger);
            }

            bassBeatArranger.Finish();
            arrangedBar.BassVoice = new ArrangedBarVoice(VoicePart.Bass);
            arrangedBar.BassVoice.BeatElements.AddRange(bassBeatArranger.GetRootBeats());

            trebleBeatArranger.Finish();
            arrangedBar.TrebleVoice = new ArrangedBarVoice(VoicePart.Treble);
            arrangedBar.TrebleVoice.BeatElements.AddRange(trebleBeatArranger.GetRootBeats());
        }

        private void AppendAndArrangeVoice(Voice voice, BeatArranger beatArranger)
        {
            if (voice == null)
                return;

            foreach (var beat in voice.Beats)
                beatArranger.AddBeat(_beatLookup[beat]);
        }

        private void ArrangeColumns(DocumentBar bar, ArrangedBar arrangedBar)
        {
            var lyricsSegmentIndex = 0;

            foreach (var segment in bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                var arrangedBeats = new List<ArrangedBarBeat>();

                this.CreateArrangedBarBeats(arrangedBeats, segment.TrebleVoice);
                this.CreateArrangedBarBeats(arrangedBeats, segment.BassVoice);

                // group all beats by sorted positions
                var groups = arrangedBeats.GroupBy(b => b.Position).OrderBy(g => g.Key);

                var isFirstBeat = true;
                foreach (var group in groups)
                {
                    var columnIndex = arrangedBar.Columns.Count;
                    var column = new ArrangedBarColumn(columnIndex);

                    foreach (var beat in group)
                    {
                        beat.Column = column;
                        column.VoiceBeats.Add(beat);
                    }

                    if (isFirstBeat)
                        column.Chord = segment.Chord;

                    isFirstBeat = false;

                    arrangedBar.Columns.Add(column);
                }

                // fill in lyrics
                if (segment.TrebleVoice != null && bar.Lyrics != null && lyricsSegmentIndex < bar.Lyrics.Segments.Count)
                {
                    foreach (var beat in segment.TrebleVoice.Beats)
                    {
                        if (lyricsSegmentIndex >= bar.Lyrics.Segments.Count)
                            break;

                        var arrangedBeat = _beatLookup[beat];
                        arrangedBeat.Column.Lyrics = bar.Lyrics.Segments[lyricsSegmentIndex];
                        ++lyricsSegmentIndex;
                    }
                }
            }

        }

        private void CreateArrangedBarBeats(List<ArrangedBarBeat> arrangedBeats, Voice voice)
        {
            if (voice == null)
                return; // todo: insert rest?

            if (voice.Beats.Count == 0)
            {
                // todo: insert rest?
            }

            var position = PreciseDuration.Zero;

            foreach (var beat in voice.Beats)
            {
                var barBeat = _beatLookup[beat];
                barBeat.Position = position;
                arrangedBeats.Add(barBeat);

                position += beat.GetDuration();
            }
        }
    }
}
