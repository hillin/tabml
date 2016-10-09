namespace TabML.Core.MusicTheory
{
    public struct Time
    {
        public int Beats { get; }
        public BaseNoteValue NoteValue { get; }

        public Time(int beats, BaseNoteValue noteValue)
        {
            this.Beats = beats;
            this.NoteValue = noteValue;
        }

        public double GetDuration()
        {
            return this.NoteValue.GetDuration() * this.Beats;
        }
    }
}
