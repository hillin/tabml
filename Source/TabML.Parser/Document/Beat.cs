using TabML.Core.MusicTheory;

namespace TabML.Parser.Document
{
    class Beat : Element
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
    }
}
