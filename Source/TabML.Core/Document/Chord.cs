using TheoreticalChord = TabML.Core.MusicTheory.Chord;

namespace TabML.Core.Document
{
    public class Chord : Element
    {
        public const int FingeringSkipString = -1;

        public string Name { get; set; }
        public ChordFingering Fingering { get; set; }
        public TheoreticalChord TheoreticalChord { get; set; }

        public Chord Clone()
        {
            return new Chord
            {
                Name = this.Name,
                Fingering = this.Fingering?.Clone(),
                TheoreticalChord = this.TheoreticalChord
            };
        }
    }
}
