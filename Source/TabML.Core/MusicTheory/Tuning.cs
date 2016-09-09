using System;

namespace TabML.Core.MusicTheory
{
    public class Tuning : IEquatable<Tuning>
    {
        public Pitch[] StringTunings { get; }

        public Tuning(params Pitch[] stringTunings)
        {
            this.StringTunings = stringTunings;
        }

        public bool Equals(Tuning other)
        {
            if (object.ReferenceEquals(other, null))
                return false;

            if (this.StringTunings.Length != other.StringTunings.Length)
                return false;

            for (var i = 0; i < this.StringTunings.Length; ++i)
            {
                if (this.StringTunings[i] != other.StringTunings[i])
                    return false;
            }

            return true;
        }
    }
}
