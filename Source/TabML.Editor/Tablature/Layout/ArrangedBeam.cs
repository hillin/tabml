using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBeam : IBeamElement
    {
        public BaseNoteValue BeatNoteValue { get; internal set; }
        public List<IBeamElement> Elements { get; }
        public int? Tuplet { get; set; }

        public ArrangedBeam(BaseNoteValue beatNoteValue)
        {
            this.BeatNoteValue = beatNoteValue;
            this.Elements = new List<IBeamElement>();
        }

        public PreciseDuration GetDuration()
        {
            return this.Elements.Sum(b => b.GetDuration());
        }
        
        public bool MatchesTuplet(ArrangedBarBeat beat)
        {
            return this.Tuplet == beat.Beat.NoteValue.Tuplet;
        }
    }
}
