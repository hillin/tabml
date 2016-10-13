using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using DocumentBar = TabML.Parser.Document.Bar;

namespace TabML.Editor.Tablature.Layout
{
    class BarArranger
    {
        private class BeatVoiceSortInfo
        {
            public PreciseDuration Position { get; set; }
            public Beat BeatVoice { get; set; }
        }


        public ArrangedBar Arrange(DocumentBar bar)
        {
            var arrangedBar = new ArrangedBar();
            foreach (var segment in bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                var sortBeats = new List<BeatVoiceSortInfo>();

                this.CreateSortBeats(sortBeats, segment.TrebleVoice);
                this.CreateSortBeats(sortBeats, segment.BassVoice);

                var groups = sortBeats.GroupBy(b => b.Position).OrderBy(g => g.Key);

                var isFirstBeat = true;
                foreach (var group in groups)
                {
                    var column = new ArrangedBarColumn();

                    column.VoiceBeats.AddRange(group.Select(g => g.BeatVoice));
                    
                    if (isFirstBeat)
                        column.Chord = segment.Chord;

                    isFirstBeat = false;

                    arrangedBar.Columns.Add(column);
                }
            }

            //todo

            return arrangedBar;
        }

        private void CreateSortBeats(List<BeatVoiceSortInfo> sortBeats, Voice voice)
        {
            if (voice.Beats.Count == 0)
            {
                // todo: insert rest?
            }

            var position = PreciseDuration.Zero;

            foreach (var beat in voice.Beats)
            {
                sortBeats.Add(new BeatVoiceSortInfo
                {
                    BeatVoice = beat,
                    Position = position,
                });

                position += beat.GetDuration();
            }
        }
    }
}
