using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using DocumentBar = TabML.Core.Document.Bar;

namespace TabML.Parser.AST
{
    class BarArranger
    {
        private readonly TablatureContext _context;
        private readonly DocumentBar _bar;
        private readonly Dictionary<VoicePart, Beat> _previousBeats;

        public BarArranger(TablatureContext context,  DocumentBar bar)
        {
            _context = context;
            _bar = bar;
            _previousBeats = new Dictionary<VoicePart, Beat>
            {
                {VoicePart.Bass, null},
                {VoicePart.Treble, null}
            };
        }

        public void Arrange()
        {
            _bar.Duration = _bar.Rhythm.GetDuration();

            this.ArrangeBeatsAndNotes();
            this.ArrangeColumns();
            this.ArrangeVoices();
        }


        private void ArrangeBeatsAndNotes()
        {
            foreach (var segment in _bar.Rhythm.Segments)
            {
                if (segment.BassVoice != null)
                {
                    this.ArrangeBeatsAndNotes(segment.BassVoice);
                }

                if (segment.TrebleVoice != null)
                {
                    this.ArrangeBeatsAndNotes(segment.TrebleVoice);
                }
            }
        }

        private void ArrangeBeatsAndNotes(RhythmSegmentVoice voice)
        {
            foreach (var beat in voice.Beats)
            {
                var previousBeat = _previousBeats[voice.Part];
                if (previousBeat != null)
                {
                    previousBeat.NextBeat = beat;
                    beat.PreviousBeat = previousBeat;
                }
                _previousBeats[voice.Part] = beat;
            }
        }

        private void ArrangeVoices()
        {
            var bassBeatArranger = new BeatArranger(_context.DocumentState.Time.NoteValue, VoicePart.Bass);
            var trebleBeatArranger = new BeatArranger(_context.DocumentState.Time.NoteValue, VoicePart.Treble);

            _bar.BassVoice = new Voice(VoicePart.Bass);
            _bar.TrebleVoice = new Voice(VoicePart.Treble);

            foreach (var segment in _bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                this.AppendAndArrangeVoice(segment.BassVoice, bassBeatArranger);
                this.AppendAndArrangeVoice(segment.TrebleVoice, trebleBeatArranger);
            }

            bassBeatArranger.Finish();
            _bar.BassVoice.BeatElements.AddRange(bassBeatArranger.GetRootBeats());

            trebleBeatArranger.Finish();
            _bar.TrebleVoice.BeatElements.AddRange(trebleBeatArranger.GetRootBeats());
        }

        private void AppendAndArrangeVoice(RhythmSegmentVoice voice, BeatArranger beatArranger)
        {
            if (voice == null)
                return;

            foreach (var beat in voice.Beats)
                beatArranger.AddBeat(beat);

            var barVoice = _bar.GetVoice(voice.Part);

            voice.LastNoteOnStrings.CopyTo(barVoice.LastNoteOnStrings, 0);
            barVoice.IsTerminatedWithRest = voice.IsTerminatedWithRest;
        }

        private void ArrangeColumns()
        {
            var lyricsSegmentIndex = 0;

            foreach (var segment in _bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                var beats = new List<Beat>();

                this.CreateArrangedBarBeats(beats, segment.TrebleVoice);
                this.CreateArrangedBarBeats(beats, segment.BassVoice);

                // group all beats by sorted positions
                var groups = beats.GroupBy(b => b.Position).OrderBy(g => g.Key);

                var isFirstBeat = true;
                foreach (var group in groups)
                {
                    var columnIndex = _bar.Columns.Count;
                    var column = new BarColumn(columnIndex);

                    foreach (var beat in group)
                    {
                        beat.OwnerColumn = column;
                        column.VoiceBeats.Add(beat);
                    }

                    if (isFirstBeat)
                        column.Chord = segment.Chord;

                    isFirstBeat = false;

                    _bar.Columns.Add(column);
                }

                // fill in lyrics
                if (segment.TrebleVoice != null
                    && _bar.Lyrics != null
                    && lyricsSegmentIndex < _bar.Lyrics.Segments.Count)
                {
                    foreach (var beat in segment.TrebleVoice.Beats)
                    {
                        if (lyricsSegmentIndex >= _bar.Lyrics.Segments.Count)
                            break;

                        beat.OwnerColumn.Lyrics = _bar.Lyrics.Segments[lyricsSegmentIndex];
                        ++lyricsSegmentIndex;
                    }
                }
            }

        }

        private void CreateArrangedBarBeats(ICollection<Beat> beats, RhythmSegmentVoice voice)
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
                beat.Position = position;
                beats.Add(beat);

                position += beat.GetDuration();
            }
        }
    }
}
