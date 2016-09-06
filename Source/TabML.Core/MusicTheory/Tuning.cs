namespace TabML.Core.MusicTheory
{
    public class Tuning
    {
        public Pitch[] StringTunings { get; }

        public Tuning(params Pitch[] stringTunings)
        {
            this.StringTunings = stringTunings;
        }
    }
}
