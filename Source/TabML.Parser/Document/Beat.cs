using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Parser.Document
{
    public class Beat : Element
    {
        public NoteValue NoteValue { get; set; }
        public BeatNote[] Notes { get; set; }
        public bool IsRest { get; set; }
        public bool IsTied { get; set; }
        public StrumTechnique StrumTechnique { get; set; } = StrumTechnique.None;
        public NoteEffectTechnique EffectTechnique { get; set; } = NoteEffectTechnique.None;
        public double EffectTechniqueParameter { get; set; }
        public NoteDurationEffect DurationEffect { get; set; } = NoteDurationEffect.None;
        public NoteAccent Accent { get; set; } = NoteAccent.Normal;


        public double GetDuration()
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
                Notes = this.Notes?.Select(n => n.Clone()).ToArray()
            };
        }
    }
}
