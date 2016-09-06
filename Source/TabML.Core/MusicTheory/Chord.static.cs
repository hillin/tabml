// ReSharper disable InconsistentNaming

namespace TabML.Core.MusicTheory
{
    partial class Chord
    {
        public static Chord Construct(string name, NoteName root, params Interval[] intervals)
        {
            var notes = new NoteName[intervals.Length + 1];
            notes[0] = root;
            for (var i = 0; i < intervals.Length; ++i)
            {
                notes[i + 1] = root + intervals[i];
            }

            return new Chord(name, notes);
        }

        public static Chord X(NoteName root)
        {
            return Chord.Construct(root.ToString(), root, Intervals.M3, Intervals.P5);
        }

        public static Chord Xm(NoteName root)
        {
            return Chord.Construct($"{root}m", root, Intervals.m3, Intervals.P5);
        }

        public static Chord Xaug(NoteName root)
        {
            return Chord.Construct($"{root}aug", root, Intervals.M3, Intervals.A5);
        }

        public static Chord Xdim(NoteName root)
        {
            return Chord.Construct($"{root}dim", root, Intervals.m3, Intervals.d5);
        }

        public static Chord Xsus2(NoteName root)
        {
            return Chord.Construct($"{root}sus2", root, Intervals.M2, Intervals.P5);
        }

        public static Chord Xsus4(NoteName root)
        {
            return Chord.Construct($"{root}sus4", root, Intervals.P4, Intervals.P5);
        }

        public static Chord X6(NoteName root)
        {
            return Chord.Construct($"{root}6", root, Intervals.M3, Intervals.P5, Intervals.M6);
        }

        public static Chord Xm6(NoteName root)
        {
            return Chord.Construct($"{root}m6", root, Intervals.m3, Intervals.P5, Intervals.M6);
        }

        public static Chord X7(NoteName root)
        {
            return Chord.Construct($"{root}7", root, Intervals.M3, Intervals.P5, Intervals.m7);
        }

        public static Chord Xmaj7(NoteName root)
        {
            return Chord.Construct($"{root}maj7", root, Intervals.M3, Intervals.P5, Intervals.M7);
        }

        public static Chord Xm7(NoteName root)
        {
            return Chord.Construct($"{root}m7", root, Intervals.m3, Intervals.P5, Intervals.m7);
        }

        public static Chord XmM7(NoteName root)
        {
            return Chord.Construct($"{root}mM7", root, Intervals.m3, Intervals.P5, Intervals.M7);
        }

        public static Chord Xdim7(NoteName root)
        {
            return Chord.Construct($"{root}dim7", root, Intervals.m3, Intervals.d5, Intervals.d7);
        }

        public static Chord X7sus2(NoteName root)
        {
            return Chord.Construct($"{root}7sus2", root, Intervals.M2, Intervals.P5, Intervals.m7);
        }

        public static Chord X7sus4(NoteName root)
        {
            return Chord.Construct($"{root}7sus4", root, Intervals.M3, Intervals.P4, Intervals.m7);
        }

        public static Chord Xmaj7sus2(NoteName root)
        {
            return Chord.Construct($"{root}maj7sus2", root, Intervals.M2, Intervals.P5, Intervals.M7);
        }

        public static Chord Xmaj7sus4(NoteName root)
        {
            return Chord.Construct($"{root}maj7sus4", root, Intervals.M3, Intervals.P4, Intervals.M7);
        }
    }
}
