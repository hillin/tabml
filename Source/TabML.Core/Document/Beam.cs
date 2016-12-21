using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    [DebuggerDisplay("{DebugString, nq}")]
    [DebuggerTypeProxy(typeof(DebugView))]
    public class Beam : VirtualElement, IInternalBeatElement, IBeatElementContainer
    {
        private class DebugView
        {
            private readonly Beam _beam;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            // ReSharper disable once UnusedMember.Local
            public IBeatElement[] Elements => _beam.Elements.ToArray();

            public DebugView(Beam beam)
            {
                _beam = beam;
            }
        }


        public BaseNoteValue BeatNoteValue { get; internal set; }
        public VoicePart VoicePart { get; }
        public bool IsRoot { get; }
        public List<IBeatElement> Elements { get; }
        public int? Tuplet { get; set; }
        public Beam OwnerBeam { get; private set; }

        public Beam(BaseNoteValue beatNoteValue, VoicePart voicePart, bool isRoot)
        {
            this.BeatNoteValue = beatNoteValue;
            this.VoicePart = voicePart;
            this.IsRoot = isRoot;
            this.Elements = new List<IBeatElement>();
        }

        public Beam(Beam owner, BaseNoteValue beatNoteValue, VoicePart voicePart)
            : this(beatNoteValue, voicePart, false)
        {
            this.OwnerBeam = owner;
        }

        public PreciseDuration GetDuration() => this.Elements.Sum(b => b.GetDuration());
        public void ClearRange()
        {
            this.Elements.ForEach(e => e.ClearRange());
        }


        public Beam Clone()
        {
            var clone = new Beam(this.BeatNoteValue, this.VoicePart, this.IsRoot)
            {
                Tuplet = this.Tuplet
            };
            foreach (var element in this.Elements.Cast<IInternalBeatElement>())
            {
                var clonedElement = element.Clone();
                clonedElement.SetOwnerBeam(clone);
                clone.Elements.Add(clonedElement);
            }

            return clone;
        }

        IInternalBeatElement IInternalBeatElement.Clone() => this.Clone();

        public bool GetIsTupletFull()
        {
            if (this.Tuplet == null)
                return false;

            return this.BeatNoteValue.GetDuration() * this.Tuplet.Value / 2 <= this.GetDuration();
        }


        public bool MatchesTuplet(Beat beat)
        {
            return this.BeatNoteValue > beat.NoteValue.Base    // if we are large enough to create a child beam for this beat
                || this.Tuplet == beat.NoteValue.Tuplet;       // or our tuplet exactly matches
        }

        void IInternalBeatElement.SetOwnerBeam(Beam owner)
        {
            this.OwnerBeam = owner;
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
