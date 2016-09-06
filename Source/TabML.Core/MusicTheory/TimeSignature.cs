namespace TabML.Core.MusicTheory
{
    public struct TimeSignature
    {
        public int Beats { get; }
        public BaseNoteValue NoteValue { get; }

        public TimeSignature(int beats, BaseNoteValue noteValue)
        {
            this.Beats = beats;
            this.NoteValue = noteValue;
        }
    }
}
