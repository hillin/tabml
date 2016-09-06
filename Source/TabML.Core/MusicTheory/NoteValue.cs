using System;

namespace TabML.Core.MusicTheory
{
    public struct NoteValue : IComparable<NoteValue>
    {
        public BaseNoteValue Base { get; }
        public NoteValueAugment Augment { get; }
        public int Tuplet { get; }

        public NoteValue(BaseNoteValue baseValue, NoteValueAugment augment = NoteValueAugment.None, int? tuplet = null)
        {
            if (tuplet != null)
            {
                if (tuplet != 1 && tuplet < 3)
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
