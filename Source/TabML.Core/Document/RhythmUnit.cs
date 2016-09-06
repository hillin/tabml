namespace TabML.Core.Document
{
    public class RhythmUnit
    {
        public MusicTheory.NoteValue NoteValue { get; set; }
        public int[] Strings { get; set; }
        public MusicTheory.StrumTechnique StrumTechnique { get; set; } = MusicTheory.StrumTechnique.None;
        public MusicTheory.NoteConnection UnitConnection { get; set; } = MusicTheory.NoteConnection.None;
        public MusicTheory.NoteEffectTechnique EffectTechnique { get; set; } = MusicTheory.NoteEffectTechnique.None;
        public double EffectTechniqueParameter { get; set; }
        public MusicTheory.NoteDurationEffect DurationEffect { get; set; } = MusicTheory.NoteDurationEffect.None;
        public MusicTheory.NoteAccent Accent { get; set; } = MusicTheory.NoteAccent.Normal;
        

        public double GetDuration()
        {
            return this.NoteValue.GetDuration();
        }
    }
}
