using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Beat : Element, IBeatElement
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
        public Beam OwnerBeam { get; set; }
        public VoicePart VoicePart { get; set; }

        public override IEnumerable<Element> Children => this.Notes;

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

        IBeatElement IBeatElement.Clone() => this.Clone();
    }
}
