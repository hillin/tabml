namespace TabML.Core.MusicTheory
{
    public struct Tempo
    {
        public BaseNoteValue NoteValue { get; }
        public int Beats { get; }

        public Tempo(int beats, BaseNoteValue noteValue = BaseNoteValue.Quater)
        {
            this.Beats = beats;
            this.NoteValue = noteValue;
        }
    }
}
