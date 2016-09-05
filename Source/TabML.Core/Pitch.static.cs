using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    partial struct Pitch
    {
        private static readonly Dictionary<int, Dictionary<BaseNoteName, Pitch>> CachedPitches
            = new Dictionary<int, Dictionary<BaseNoteName, Pitch>>();
        

        public static Pitch Resolve(int semitones, int? degreeToSnap)
        {
            var octaves = semitones / 12;
            var normalizedSemitones = semitones % 12;

            if (degreeToSnap == null)
                return new Pitch(NoteNames.FromSemitones(normalizedSemitones), octaves, Accidental.Natural);

            var noteSemitones = degreeToSnap.Value

            if (noteSemitones == normalizedSemitones)
                return new Pitch(noteName.Value, octaves, Accidental.Natural);

            if (noteSemitones == normalizedSemitones - 1)
                return new Pitch(noteName.Value, octaves, Accidental.Sharp);

            if (noteSemitones == normalizedSemitones + 1)
                return new Pitch(noteName.Value, octaves, Accidental.Flat);

            throw new ArgumentException("cannot resolve to specified NoteName", nameof(noteName));
        }
    }
}
