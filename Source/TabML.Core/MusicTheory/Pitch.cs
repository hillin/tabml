using System;

namespace TabML.Core.MusicTheory
{
    public partial struct Pitch : IEquatable<Pitch>
    {


        public override int GetHashCode()
        {
            unchecked
            {
                return (this.NoteName.GetHashCode() * 397) ^ this.OctaveGroup;
            }
        }

        public const int NeutralOctaveGroup = -1;

        public NoteName NoteName { get; }
        public int OctaveGroup { get; }

        public Pitch(NoteName noteName, int octaveGroup = NeutralOctaveGroup)
        {
            this.NoteName = noteName;
            this.OctaveGroup = octaveGroup;
        }

        public Pitch WithOctave(int octaveGroup)
        {
            return new Pitch(this.NoteName, octaveGroup);
        }

        public int GetNormalizedSemitones()
        {
            return this.NoteName.GetSemitones();
        }

        public int GetSemitones()
        {
            return this.OctaveGroup * 12 + this.NoteName.GetSemitones();
        }

        public bool Equals(Pitch other)
        {
            return this.NoteName.Equals(other.NoteName) && this.OctaveGroup == other.OctaveGroup;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
                return false;

            return obj is Pitch && this.Equals((Pitch)obj);
        }

        public override string ToString()
        {
            return this.NoteName.ToString();
        }

        public string ToLongString()
        {
            return $"{this.NoteName}{this.OctaveGroup}";
        }

        public bool EnharmoniclyEquals(Pitch other)
        {
            return this.OctaveGroup == other.OctaveGroup &&
                   this.GetNormalizedSemitones() == other.GetNormalizedSemitones();
        }

        public static Pitch operator +(Pitch pitch, Interval interval)
        {
            if (pitch.NoteName.Accidental.GetIsDoubleAccidental())
                throw new ArgumentException(
                    "Pitch + Interval operator is not available to pitches with double accidental",
                    nameof(pitch));

            var semitones = pitch.GetSemitones() + interval.GetSemitoneOffset();
            var degrees = pitch.NoteName.BaseName.GetAbsoluteDegrees() + interval.NormalizedNumber;
            return Pitch.Resolve(semitones, degrees);
        }

        public static bool operator ==(Pitch p1, Pitch p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pitch p1, Pitch p2)
        {
            return !p1.Equals(p2);
        }
    }
}
