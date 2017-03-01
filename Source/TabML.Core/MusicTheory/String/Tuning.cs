using System;
using System.Linq;

namespace TabML.Core.MusicTheory.String
{
    public class Tuning : IEquatable<Tuning>
    {

        public string Name { get; }
        public Pitch[] StringTunings { get; }

        public Tuning(string name, params Pitch[] stringTunings)
        {
            this.Name = name;
            this.StringTunings = stringTunings;
        }

        public bool Equals(Tuning other)
        {
            if (object.ReferenceEquals(other, null))
                return false;

            if (this.StringTunings.Length != other.StringTunings.Length)
                return false;

            return !this.StringTunings.Where((t, i) => t != other.StringTunings[i]).Any();
        }

        public bool InOctaveEquals(Tuning other)
        {
            if (object.ReferenceEquals(other, null))
                return false;

            if (this.StringTunings.Length != other.StringTunings.Length)
                return false;

            return !this.StringTunings.Where((t, i) => t.NoteName != other.StringTunings[i].NoteName).Any();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Tuning;
            return !object.ReferenceEquals(other, null) && this.Equals(other);
        }

        public static bool operator ==(Tuning t1, Tuning t2)
        {
            if (object.ReferenceEquals(t1, null))
                return object.ReferenceEquals(t2, null);

            if (object.ReferenceEquals(t2, null))
                return false;

            return t1.Equals(t2);
        }

        public static bool operator !=(Tuning t1, Tuning t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Name?.GetHashCode() ?? 0) * 397) ^ (this.StringTunings?.GetHashCode() ?? 0);
            }
        }

    }
}
