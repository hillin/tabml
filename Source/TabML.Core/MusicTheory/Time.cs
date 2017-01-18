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

        public PreciseDuration GetDuration()
        {
            return this.NoteValue.GetDuration() * this.Beats;
        }

        public static bool operator ==(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(Time t1, Time t2)
        {
            return !t1.Equals(t2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Time)
                return this.Equals((Time)obj);

            return false;
        }

        public bool Equals(Time other)
        {
            return this.Beats == other.Beats && this.NoteValue == other.NoteValue;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Beats * 397) ^ (int)this.NoteValue;
            }
        }
    }
}
