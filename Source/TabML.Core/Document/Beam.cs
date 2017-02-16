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
        public VoicePart VoicePart => this.OwnerVoice.VoicePart;
        public Bar OwnerBar => this.OwnerVoice.OwnerBar;
        public Voice OwnerVoice { get; }
        public bool IsRoot { get; }
        public List<IBeatElement> Elements { get; }
        public int? Tuplet { get; set; }
        public Beam RootBeam => this.IsRoot ? this : this.OwnerBeam?.RootBeam;
        public Beam OwnerBeam => this.BeatElementOwner as Beam;
        internal IBeatElementContainer BeatElementOwner { get; private set; }

        public Beam(BaseNoteValue beatNoteValue, Voice ownerVoice, bool isRoot)
        {
            this.BeatNoteValue = beatNoteValue;
            this.OwnerVoice = ownerVoice;
            this.IsRoot = isRoot;
            this.Elements = new List<IBeatElement>();
        }

        internal Beam(Beam owner, BaseNoteValue beatNoteValue, Voice ownerVoice)
            : this(beatNoteValue, ownerVoice, false)
        {
            this.BeatElementOwner = owner;
        }

        public PreciseDuration GetDuration() => this.Elements.Sum(b => b.GetDuration());
        public void ClearRange()
        {
            this.Elements.ForEach(e => e.ClearRange());
        }

        void IInternalBeatElement.SetOwner(IBeatElementContainer owner)
        {
            this.BeatElementOwner = owner;
        }

        public Beam Clone()
        {
            var clone = new Beam(this.BeatNoteValue, this.OwnerVoice, this.IsRoot)
            {
                Tuplet = this.Tuplet
            };
            foreach (var element in this.Elements.Cast<IInternalBeatElement>())
            {
                var clonedElement = element.Clone();
                clonedElement.SetOwner(clone);
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
