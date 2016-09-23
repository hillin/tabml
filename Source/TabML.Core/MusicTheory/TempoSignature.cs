namespace TabML.Core.MusicTheory
{
    public struct TempoSignature
    {
        public BaseNoteValue NoteValue { get; }
        public int Beats { get; }

        public TempoSignature(int beats, BaseNoteValue noteValue = BaseNoteValue.Quater)
        {
            this.Beats = beats;
            this.NoteValue = noteValue;
        }
    }
}
