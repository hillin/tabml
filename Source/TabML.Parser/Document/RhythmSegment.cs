using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.Document
{
    class RhythmSegment : Element
    {
        public Chord Chord { get; set; }
        public List<Voice> Voices { get; }
        public bool IsOmittedByTemplate { get; set; }

        public RhythmSegment()
        {
            this.Voices = new List<Voice>();
        }
        public double GetDuration()
        {
            return this.Voices.Max(v => v.GetDuration());
        }

        public void ClearRange()
        {
            this.Range = null;
            foreach (var voice in this.Voices)
                voice.ClearRange();
        }

        public RhythmSegment Clone()
        {
            var clone = new RhythmSegment
            {
                Range = this.Range,
                Chord = this.Chord.Clone(),
                IsOmittedByTemplate = this.IsOmittedByTemplate
            };

            clone.Voices.AddRange(this.Voices.Select(v => v.Clone()));

            return clone;
        }
    }
}
