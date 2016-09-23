using System;

namespace TabML.Core.MusicTheory
{
    public struct NoteValue : IComparable<NoteValue>
    {
        public static bool IsValidTuplet(int tuplet)
        {
            if (tuplet < 3)
                return false;

            if (tuplet > 64)
                return false;

            var log = Math.Log(tuplet, 2);
            if (log - (int)log < 0.001)
                return false;

            return true;
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
    }
}
