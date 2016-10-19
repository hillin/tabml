﻿using System;
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

        private readonly Dictionary<Beat, ArrangedBarBeat> _beatLookup;

        public BarArranger()
        {
            _beatLookup = new Dictionary<Beat, ArrangedBarBeat>();
        }

        public ArrangedBar Arrange(DocumentBar bar)
        {
            var arrangedBar = new ArrangedBar();
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
            var bassVoice = new ArrangedBarVoice();
            var trebleVoice = new ArrangedBarVoice();

            foreach (var segment in bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                this.AppendAndArrangeVoice(bar, segment.BassVoice, bassVoice);
                this.AppendAndArrangeVoice(bar, segment.TrebleVoice, trebleVoice);
            }
        }

        private void AppendAndArrangeVoice(DocumentBar bar, Voice voice, ArrangedBarVoice arrangedVoice)
        {
            if (voice == null)
                return;

            ArrangedBeam beam;

            if (arrangedVoice.Beams.Count == 0)
                arrangedVoice.Beams.Add(beam = new ArrangedBeam());
            else
            {
                var lastBeam = arrangedVoice.Beams[arrangedVoice.Beams.Count - 1];
                if(lastBeam.GetDuration() < bar.DocumentState.TimeSignature)
            }
        }

        private void ArrangeColumns(DocumentBar bar, ArrangedBar arrangedBar)
        {
            foreach (var segment in bar.Rhythm.Segments)
            {
                if (segment.IsOmittedByTemplate)
                    continue;

                var arrangedBeats = new List<ArrangedBarBeat>();

                this.CreateArrangedBarBeats(arrangedBeats, segment.TrebleVoice);
                this.CreateArrangedBarBeats(arrangedBeats, segment.BassVoice);

                var groups = arrangedBeats.GroupBy(b => b.Position).OrderBy(g => g.Key);

                var isFirstBeat = true;
                foreach (var group in groups)
                {
                    var column = new ArrangedBarColumn { Position = @group.Key };

                    column.VoiceBeats.AddRange(group);

                    if (isFirstBeat)
                        column.Chord = segment.Chord;

                    isFirstBeat = false;

                    arrangedBar.Columns.Add(column);
                }
            }
        }

        private void CreateArrangedBarBeats(List<ArrangedBarBeat> arrangedBeats, Voice voice)
        {
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
