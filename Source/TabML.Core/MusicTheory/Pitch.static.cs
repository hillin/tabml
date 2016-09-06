namespace TabML.Core.MusicTheory
{
    partial struct Pitch
    {
        public static Pitch Resolve(int semitones, int? degreeToSnap)
        {
            return new Pitch(NoteName.FromSemitones(semitones, degreeToSnap), semitones / 12);
        }
    }
}
