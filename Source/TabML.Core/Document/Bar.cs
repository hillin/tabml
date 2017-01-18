using System;
using System.Collections.Generic;
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

        #region Basic structures
        /// <summary>
        /// The index of the bar within the entire tablature
        /// </summary>
        public int Index { get; set; }
        public OpenBarLine? OpenLine { get; set; }
        public CloseBarLine? CloseLine { get; set; }
        public Rhythm Rhythm { get; set; }
        public Lyrics Lyrics { get; set; }
        public DocumentState DocumentState { get; set; }

        public override IEnumerable<Element> Children
        {
            get
            {
                yield return this.Rhythm;
                yield return this.Lyrics;
            }
        }

        #endregion

        #region Arranged structures

        public Voice TrebleVoice { get; set; }
        public Voice BassVoice { get; set; }
        public PreciseDuration Duration { get; set; }
        public List<BarColumn> Columns { get; }

        #endregion

        #region States

        public Bar PreviousBar { get; set; }
        public Bar NextBar { get; set; }

        /// <summary>
        /// The logically previous bar of this bar. For most bars, it's the previous neighbor on the tablature, but
        /// for bars starting an alternation, it could be a remote one
        /// </summary>
        public Bar LogicalPreviousBar { get; set; }

        public AlternativeEndingPosition AlternativeEndingPosition
        {
            get
            {
                var alternation = this.DocumentState.CurrentAlternation;

                if (alternation == null)
                    return AlternativeEndingPosition.None;

                var isStart = this.PreviousBar == null
                              || this.PreviousBar.DocumentState.CurrentAlternation != alternation;

                var isEnd = this.NextBar == null
                    || this.NextBar.DocumentState.CurrentAlternation != alternation;

                if (isStart)
                    return isEnd ? AlternativeEndingPosition.StartAndEnd : AlternativeEndingPosition.Start;

                if (isEnd)
                    return AlternativeEndingPosition.End;

                return AlternativeEndingPosition.Inside;
            }
        }

        #endregion

        public Bar()
        {
            this.Columns = new List<BarColumn>();
        }

        public Voice GetVoice(VoicePart part)
        {
            switch (part)
            {
                case VoicePart.Treble:
                    return this.TrebleVoice;
                case VoicePart.Bass:
                    return this.BassVoice;
                default:
                    throw new ArgumentOutOfRangeException(nameof(part), part, null);
            }
        }


    }
}
