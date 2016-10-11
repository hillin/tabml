using System;
using TabML.Core.MusicTheory;

namespace TabML.Parser.Document
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

        public OpenBarLine? OpenLine { get; set; }
        public CloseBarLine? CloseLine { get; set; }
        public Rhythm Rhythm { get; set; }
        public Lyrics Lyrics { get; set; }
        public DocumentState DocumentState { get; set; }
    }
}
