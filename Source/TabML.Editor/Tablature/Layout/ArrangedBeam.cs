using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    [DebuggerDisplay("{DebugString, nq}")]
    [DebuggerTypeProxy(typeof(DebugView))]
    class ArrangedBeam : IBeamElement
    {
        private class DebugView
        {
            private readonly ArrangedBeam _beam;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            // ReSharper disable once UnusedMember.Local
            public IBeamElement[] Elements => _beam.Elements.ToArray();

            public DebugView(ArrangedBeam beam)
            {
                _beam = beam;
            }
        }

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

#if DEBUG
        private string DebugString
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("Beam of ");
                builder.Append(this.BeatNoteValue);

                if (this.Tuplet != null)
                    builder.Append('/').Append(this.Tuplet);

                builder.Append('(').Append(this.GetDuration()).Append(')');

                return builder.ToString();
            }
        }
#endif
    }
}
