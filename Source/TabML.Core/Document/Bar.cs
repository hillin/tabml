using System;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Bar : Element
    {
        public static BarLine MergeBarLine(CloseBarLine? close, OpenBarLine? open)
        {
            switch (open)
            {
                case null:
                case OpenBarLine.Standard:
                    if (close == null)
                        return BarLine.Standard;

                    return (BarLine)close.Value;

                case OpenBarLine.Double:
                    switch (close)
                    {
                        case null:
                            return BarLine.Double;
                        case CloseBarLine.Standard:
                            return BarLine.Double;
                        case CloseBarLine.Double:
                            return BarLine.Double;
                        case CloseBarLine.End:
                            return BarLine.End;
                        case CloseBarLine.EndRepeat:
                            return BarLine.EndRepeat;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(close), close, null);
                    }

                case OpenBarLine.BeginRepeat:
                    switch (close)
                    {
                        case null:
                            return BarLine.BeginRepeat;
                        case CloseBarLine.Standard:
                            return BarLine.BeginRepeat;
                        case CloseBarLine.Double:
                            return BarLine.BeginRepeat;
                        case CloseBarLine.End:
                            return BarLine.BeginRepeatAndEnd;
                        case CloseBarLine.EndRepeat:
                            return BarLine.BeginAndEndRepeat;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(close), close, null);
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(open), open, null);
            }
        }

        public static BarLine MergeBarLine(Bar bar1, Bar bar2)
        {
            if (bar1 == null && bar2 == null)
                throw new ArgumentException("two bars cannot be both null", nameof(bar1));

            if (bar1 == null)
            {
                if (bar2.OpenLine == null)
                    return BarLine.Standard;

                return (BarLine)bar2.OpenLine.Value;
            }

            if (bar2 == null)
            {
                if (bar1.CloseLine == null)
                    return BarLine.Standard;

                return (BarLine)bar1.CloseLine.Value;
            }

            return Bar.MergeBarLine(bar1.CloseLine, bar2.OpenLine);
        }

        /// <summary>
        /// The index of the bar within the entire tablature
        /// </summary>
        public int Index { get; set; }
        public OpenBarLine? OpenLine { get; set; }
        public CloseBarLine? CloseLine { get; set; }
        public Rhythm Rhythm { get; set; }
        public Lyrics Lyrics { get; set; }
        public DocumentState DocumentState { get; set; }

        /// <summary>
        /// whether the treble voice is rested at the end of the bar. this propery is graduately updated to reflect
        /// the rest state of the treble voice, in order to determine whether a note can be tied to a previous note
        /// </summary>
        public bool TrebleRested { get; set; }

        /// <summary>
        /// whether the bass voice is rested at the end of the bar. this propery is graduately updated to reflect
        /// the rest state of the bass voice, in order to determine whether a note can be tied to a previous note
        /// </summary>
        public bool BassRested { get; set; }

        public BeatNote[] LastNoteOnStrings { get; } = new BeatNote[Defaults.Strings];
        /// <summary>
        /// The logically previous bar of this bar. For most bars, it's the previous neighbor on the tablature, but
        /// for bars starting an alternation, it could be a remote one
        /// </summary>
        public Bar LogicalPreviousBar { get; set; }

        public PreciseDuration GetDuration() => this.Rhythm.GetDuration();

        public void SetVoiceRestedState(VoicePart voice, bool rested)
        {
            switch (voice)
            {
                case VoicePart.Treble:
                    this.TrebleRested = rested;
                    break;
                case VoicePart.Bass:
                    this.BassRested = rested;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voice), voice, null);
            }
        }

        public bool GetVoiceRestedState(VoicePart voice)
        {
            switch (voice)
            {
                case VoicePart.Treble:
                    return this.TrebleRested;
                case VoicePart.Bass:
                    return this.BassRested;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voice), voice, null);
            }
        }
    }
}
