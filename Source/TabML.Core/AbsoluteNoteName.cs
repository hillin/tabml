using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class AbsoluteNoteName : IEquatable<AbsoluteNoteName>
    {
        public const int NeutralOctaveGroup = 0;

        public NoteName NoteName { get; }
        public int OctaveGroup { get; }
        public Accidental Accidental { get; }

        internal AbsoluteNoteName(NoteName noteName, int octaveGroup = NeutralOctaveGroup, Accidental accidental = Accidental.Natural)
        {
            NoteName = noteName;
            Accidental = accidental;
            OctaveGroup = octaveGroup;
        }

        public AbsoluteNoteName WithOctave(int octaveGroup)
        {
            return new AbsoluteNoteName(this.NoteName, octaveGroup, this.Accidental);
        }

        public int GetTET12Value()
        {
            return (this.NoteName.GetTET12Value() + this.Accidental.GetTET12Offset()) % 12;
        }

        public override string ToString()
        {
            switch (this.Accidental)
            {
                case Accidental.Natural:
                    return this.NoteName.ToString();
                case Accidental.Sharp:
                    return $"{this.NoteName}♯";
                case Accidental.Flat:
                    return $"{this.NoteName}♭";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string ToLongString()
        {
            var baseString = $"{this.Accidental}{this.OctaveGroup}";
            switch (this.Accidental)
            {
                case Accidental.Natural:
                    return baseString;
                case Accidental.Sharp:
                    return $"{baseString}♯";
                case Accidental.Flat:
                    return $"{baseString}♭";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Equals(AbsoluteNoteName other)
        {
            return this.OctaveGroup == other.OctaveGroup && this.GetTET12Value() == other.GetTET12Value();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is AbsoluteNoteName && this.Equals((AbsoluteNoteName)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                return this.OctaveGroup * 12 + this.GetTET12Value();
            }
        }
    }
}
