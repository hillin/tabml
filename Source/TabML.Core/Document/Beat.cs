using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Beat : Element, IInternalBeatElement
    {
        public NoteValue NoteValue
        {
            get;
            set;
        }
        public BeatNote[] Notes { get; set; }
        public bool IsRest { get; set; }
        public bool IsTied { get; set; }
        public StrumTechnique StrumTechnique { get; set; } = StrumTechnique.None;
        public BeatEffectTechnique EffectTechnique { get; set; } = BeatEffectTechnique.None;
        public double EffectTechniqueParameter { get; set; }
        public BeatDurationEffect DurationEffect { get; set; } = BeatDurationEffect.None;
        public BeatAccent Accent { get; set; } = BeatAccent.Normal;
        public Beat PreviousBeat { get; set; }
        public Beat NextBeat { get; set; }
        public PreciseDuration Position { get; set; }
        public BarColumn OwnerColumn { get; set; }
        public Beam OwnerBeam { get; private set; }
        public VoicePart VoicePart { get; set; }

        public PreBeatConnection PreConnection { get; set; }
        public PostBeatConnection PostConnection { get; set; }
        public TiePosition? TiePosition { get; set; }

        public override IEnumerable<Element> Children => this.Notes;

        public Beat GetTieHead()
        {
            var beat = this;
            while (beat.IsTied && beat.PreviousBeat != null)
                beat = beat.PreviousBeat;

            return beat;
        }

        public PreciseDuration GetDuration()
        {
            return this.NoteValue.GetDuration();
        }

        public void ClearRange()
        {
            this.Range = null;
            foreach (var note in this.Notes)
                note.ClearRange();
        }

        public Beat Clone()
        {
            return new Beat
            {
                Range = this.Range,
                NoteValue = this.NoteValue,
                IsRest = this.IsRest,
                IsTied = this.IsTied,
                PreConnection = this.PreConnection,
                TiePosition = this.TiePosition,
                PostConnection = this.PostConnection,
                StrumTechnique = this.StrumTechnique,
                EffectTechnique = this.EffectTechnique,
                EffectTechniqueParameter = this.EffectTechniqueParameter,
                DurationEffect = this.DurationEffect,
                Accent = this.Accent,
                Notes = this.Notes?.Select(n => n.Clone()).ToArray(),
                OwnerBeam = this.OwnerBeam,
                VoicePart = this.VoicePart
            };
        }

        void IInternalBeatElement.SetOwnerBeam(Beam owner)
        {
            this.OwnerBeam = owner;
        }


        IInternalBeatElement IInternalBeatElement.Clone() => this.Clone();
    }
}
