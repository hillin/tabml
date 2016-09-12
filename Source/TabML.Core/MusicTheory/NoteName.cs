using System;
using System.Collections.Generic;

namespace TabML.Core.MusicTheory
{
    public struct NoteName : IEquatable<NoteName>
    {


        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)this.BaseName * 397) ^ (int)this.Accidental;
            }
        }

        private static readonly NoteName[] SemitoneToNoteNameLookup = { NoteNames.C, NoteNames.CSharp, NoteNames.D, NoteNames.DSharp, NoteNames.E, NoteNames.F, NoteNames.FSharp, NoteNames.G, NoteNames.GSharp, NoteNames.A, NoteNames.ASharp, NoteNames.B };


        private static readonly Dictionary<int, NoteName>[] SemitoneToNoteNameSnappedToDegreeLookup =
        {
            /*0*/ new Dictionary<int, NoteName> {[6] = NoteNames.BSharp, [0] = NoteNames.C, [1] = NoteNames.DDoubleFlat},
            /*1*/ new Dictionary<int, NoteName> {[6] = NoteNames.BDoubleSharp, [0] = NoteNames.CSharp, [1] = NoteNames.DFlat},
            /*2*/ new Dictionary<int, NoteName> {[0] = NoteNames.CDoubleSharp, [1] = NoteNames.D, [2] = NoteNames.EDoubleFlat},
            /*3*/ new Dictionary<int, NoteName> {[1] = NoteNames.DSharp, [2] = NoteNames.EFlat, [3] = NoteNames.FDoubleFlat},
            /*4*/ new Dictionary<int, NoteName> {[1] = NoteNames.DDoubleSharp, [2] = NoteNames.E, [3] = NoteNames.FFlat},
            /*5*/ new Dictionary<int, NoteName> {[2] = NoteNames.ESharp, [3] = NoteNames.F, [4] = NoteNames.GDoubleFlat},
            /*6*/ new Dictionary<int, NoteName> {[2] = NoteNames.EDoubleSharp, [3] = NoteNames.FSharp, [4] = NoteNames.GFlat},
            /*7*/ new Dictionary<int, NoteName> {[3] = NoteNames.FDoubleSharp, [4] = NoteNames.G, [5] = NoteNames.ADoubleFlat},
            /*8*/ new Dictionary<int, NoteName> {[4] = NoteNames.GSharp, [5] = NoteNames.AFlat},
            /*9*/ new Dictionary<int, NoteName> {[4] = NoteNames.GDoubleSharp, [5] = NoteNames.A, [6] = NoteNames.BDoubleFlat},
            /*10*/ new Dictionary<int, NoteName> {[5] = NoteNames.ASharp, [6] = NoteNames.BFlat, [0] = NoteNames.CDoubleFlat},
            /*11*/ new Dictionary<int, NoteName> {[5] = NoteNames.ADoubleSharp, [6] = NoteNames.B, [0] = NoteNames.CFlat},
        };

        public static NoteName FromSemitones(int semitones, int? degreeToSnap)
        {
            semitones = semitones % 12;

            if (!degreeToSnap.HasValue)
                return SemitoneToNoteNameLookup[semitones];

            var lookup = SemitoneToNoteNameSnappedToDegreeLookup[semitones];
            NoteName noteName;
            if (lookup.TryGetValue(degreeToSnap.Value % 7, out noteName))
                return noteName;

            throw new ArgumentException("cannot resolve to specified degree", nameof(degreeToSnap));
        }

        public BaseNoteName BaseName { get; }
        public Accidental Accidental { get; }

        public NoteName(BaseNoteName baseName, Accidental accidental)
        {
            this.BaseName = baseName;
            this.Accidental = accidental;
        }

        public int GetSemitones()
        {
            return (this.BaseName.GetSemitones() + this.Accidental.GetSemitoneOffset() + 12) % 12;
        }

        public bool Equals(NoteName other)
        {
            return this.BaseName == other.BaseName && this.Accidental == other.Accidental;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
                return false;
            return obj is NoteName && this.Equals((NoteName)obj);
        }

        public bool EnharmoniclyEquals(NoteName other)
        {
            return this.GetSemitones() == other.GetSemitones();
        }

        public static bool operator ==(NoteName name1, NoteName name2)
        {
            return name1.Equals(name2);
        }

        public static bool operator !=(NoteName name1, NoteName name2)
        {
            return !name1.Equals(name2);
        }

        public override string ToString()
        {
            switch (this.Accidental)
            {
                case Accidental.Natural:
                    return this.BaseName.ToString();
                case Accidental.Sharp:
                    return $"{this.BaseName}♯";
                case Accidental.Flat:
                    return $"{this.BaseName}♭";
                case Accidental.DoubleSharp:
#if USE_UNICODE_EXTENDED_ACCIDENTAL_SYMBOLS
                    return $"{this.BaseName}\u1d12a";
#else
                    return $"{this.BaseName}♯♯";
#endif
                case Accidental.DoubleFlat:
#if USE_UNICODE_EXTENDED_ACCIDENTAL_SYMBOLS
                    return $"{this.BaseName}\u1d12b";
#else
                    return $"{this.BaseName}♭♭";
#endif
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
