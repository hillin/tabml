using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class RhythmSegmentVoice : Element
    {
        public List<Beat> Beats { get; }
        public VoicePart Part { get; }

        private bool _isTerminatedWithRest;
        public bool IsTerminatedWithRest
        {
            get { return _isTerminatedWithRest; }
            set
            {
                _isTerminatedWithRest = value;
                if (_isTerminatedWithRest)
                    for (var i = 0; i < this.LastNoteOnStrings.Length; ++i)
                        this.LastNoteOnStrings[i] = null;
            }
        }

        /// <summary>
        /// the last notes appeared on each string of this voice. this propery is graduately updated to reflect
        /// the state of the voice, in order to determine the predecessor note to be tied to another note
        /// </summary>
        public BeatNote[] LastNoteOnStrings { get; } = new BeatNote[Defaults.Strings];


        public override IEnumerable<Element> Children => this.Beats;

        public RhythmSegmentVoice(VoicePart part)
        {
            this.Part = part;
            this.Beats = new List<Beat>();
        }
        public PreciseDuration GetDuration() => this.Beats.Sum(n => n.GetDuration());

        public void ClearRange()
        {
            this.Range = null;

            foreach (var beat in this.Beats)
                beat.ClearRange();
        }

        public RhythmSegmentVoice Clone()
        {
            var clone = new RhythmSegmentVoice(this.Part)
            {
                Range = this.Range,
            };
            clone.Beats.AddRange(this.Beats.Select(b => b.Clone()));
            return clone;
        }
    }
}
