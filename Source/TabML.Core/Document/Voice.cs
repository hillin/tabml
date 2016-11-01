using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Voice : Element
    {
        public List<Beat> Beats { get; }
        public VoicePart Part { get; set; }

        public Voice()
        {
            this.Beats = new List<Beat>();
        }
        public PreciseDuration GetDuration() => this.Beats.Sum(n => n.GetDuration());

        public void ClearRange()
        {
            this.Range = null;

            foreach (var beat in this.Beats)
                beat.ClearRange();
        }

        public Voice Clone()
        {
            var clone = new Voice
            {
                Range = this.Range,
                Part = this.Part
            };
            clone.Beats.AddRange(this.Beats.Select(b => b.Clone()));
            return clone;
        }
    }
}
