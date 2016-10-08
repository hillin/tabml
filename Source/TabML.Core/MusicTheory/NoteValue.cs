using System;

namespace TabML.Core.MusicTheory
{
    public struct NoteValue : IComparable<NoteValue>
    {
        public static bool TryResolveFromDuration(double duration, out NoteValue noteValue, bool complex = false)
        {
            const double tolerance = 1e-7;

            for (var baseNoteValue = BaseNoteValue.Large; baseNoteValue >= BaseNoteValue.TwoHundredFiftySixth; --baseNoteValue)
            {
                if (Math.Abs(duration - baseNoteValue.GetDuration()) > tolerance)
                    continue;

                noteValue = new NoteValue(baseNoteValue);
                return true;
            }

            if (complex)
            {
                var searchAugments = new[] { NoteValueAugment.Dot, NoteValueAugment.TwoDots, NoteValueAugment.ThreeDots };
                var searchTuplets = new[] { 3, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15 };

                for (var baseNoteValue = BaseNoteValue.Large; baseNoteValue >= BaseNoteValue.TwoHundredFiftySixth; --baseNoteValue)
                {
                    var baseDuration = baseNoteValue.GetDuration();
                    var baseInvertedDuration = (double)baseNoteValue.GetInvertedDuration();

                    foreach (var augment in searchAugments)
                    {
                        var augmentedDuration = baseDuration * augment.GetDurationMultiplier();
                        if (Math.Abs(duration - augmentedDuration) < tolerance)
                        {

                            noteValue = new NoteValue(baseNoteValue, augment);
                            return true;
                        }

                        foreach (var tuplet in searchTuplets)
                        {
                            if (Math.Abs(duration - augmentedDuration * (baseInvertedDuration / tuplet)) < tolerance)
                            {
                                noteValue = new NoteValue(baseNoteValue, augment, tuplet);
                                return true;
                            }
                        }
                    }


                    noteValue = new NoteValue(baseNoteValue);
                    return true;
                }
            }

            noteValue = default(NoteValue);
            return false;
        }

        public static bool IsValidTuplet(int tuplet)
        {
            if (tuplet < 3)
                return false;

            if (tuplet > 64)
                return false;

            var log = Math.Log(tuplet, 2);
            return !(log - (int)log < 0.001);
        }

        public BaseNoteValue Base { get; }
        public NoteValueAugment Augment { get; }
        public int Tuplet { get; }

        public NoteValue(BaseNoteValue baseValue, NoteValueAugment augment = NoteValueAugment.None, int? tuplet = null)
        {
            if (tuplet != null)
            {
                if (!NoteValue.IsValidTuplet(tuplet.Value))
                    throw new ArgumentOutOfRangeException(nameof(tuplet), "invalid tuplet value");

                if (baseValue >= 0)
                    throw new ArgumentOutOfRangeException(nameof(tuplet),
                        "tuplet not supported for notes equal or longer than a whole");
            }

            this.Base = baseValue;
            this.Augment = augment;
            this.Tuplet = tuplet ?? baseValue.GetInvertedDuration();
        }

        public double GetDuration()
        {
            var baseDuration = this.Base.GetDuration();
            return baseDuration * this.Augment.GetDurationMultiplier() * ((double)this.Base.GetInvertedDuration() / this.Tuplet);
        }

        public int CompareTo(NoteValue other)
        {
            return this.GetDuration().CompareTo(other.GetDuration());
        }

        public double GetBeats(BaseNoteValue beatLength)
        {
            return this.GetDuration() / beatLength.GetDuration();
        }
    }
}
