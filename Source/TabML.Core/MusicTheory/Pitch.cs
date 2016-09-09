﻿using System;

namespace TabML.Core.MusicTheory
{
    public partial struct Pitch : IEquatable<Pitch>
    {
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
            return this.NoteName == other.NoteName
                   && this.OctaveGroup == other.OctaveGroup;
        }

        public override string ToString()
        {
            return this.NoteName.ToString();
        }

        public string ToLongString()
        {
            return $"{this.NoteName}{this.OctaveGroup}";
        }

        public bool SemitoneEquals(Pitch other)
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


    }
}
