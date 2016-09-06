using System;

namespace TabML.Core.Document
{
    public class Bar
    {
        public static BarLine MergeBarLine(CloseBarLine close, OpenBarLine open)
        {
            switch (open)
            {
                case OpenBarLine.Standard:
                    return (BarLine)close;
                case OpenBarLine.Double:
                    switch (close)
                    {
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
                return (BarLine)bar2.OpenLine;

            if (bar2 == null)
                return (BarLine)bar1.CloseLine;

            return Bar.MergeBarLine(bar1.CloseLine, bar2.OpenLine);
        }

        public OpenBarLine OpenLine { get; set; }
        public CloseBarLine CloseLine { get; set; }
        public BarVoice[] Voices { get; set; }
    }
}
