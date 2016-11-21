using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TabML.Core
{
    [DebuggerDisplay("{From} - {To}")]
    [DebuggerTypeProxy(typeof(TextRange.DebugView))]
    public struct TextRange
    {
        internal class DebugView
        {
            private readonly TextRange _textRange;

            public DebugView(TextRange textRange)
            {
                _textRange = textRange;
            }

            public string Content
            {
                get
                {
#if DEBUG
                    return _textRange.Content;
#else
                    return "(only available in debug mode)";
#endif
                }
            }
        }


        public TextPointer From { get; set; }   //inclusive
        public TextPointer To { get; set; }     //exclusive

#if DEBUG
        internal TextSource Source { get; set; }

        public string Content => this.Source == null
                               ? "(source unavailable)"
                               : this.Source.Substring(this);
#endif

        public TextRange(TextPointer from, TextPointer to)
            : this(from, to, null)
        {
        }

        internal TextRange(TextPointer from, TextPointer to, TextSource source)
        {
            this.From = @from;
            this.To = to;
#if DEBUG
            this.Source = source;
#endif
        }

        internal TextRange(TextPointer position, TextSource source)
            : this(position, position, source)
        {

        }

        internal TextRange(TextPointer from, int length, TextSource source)
            : this(from, new TextPointer(from.Row, from.Column + length), source)
        {

        }

        internal TextRange(TextPointer @base, int offset, int length, TextSource source)
            : this(new TextPointer(@base.Row, @base.Column + offset), length, source)
        {

        }

        internal TextRange(TextRange readRange, Capture capture, TextSource source)
            : this(readRange.From, capture.Index, capture.Length, source)
        {

        }

        public TextRange Offset(TextPointer @base)
        {
            return new TextRange(this.From.Offset(@base), this.To.Offset(@base), this.Source);
        }

        public TextRange Extend(int size)
        {
            return new TextRange(this.From, this.To.OffsetColumn(size), this.Source);
        }

        public TextRange Union(TextRange range)
        {
            return new TextRange(this.From > range.From ? range.From : this.From,
                                 this.To > range.To ? this.To : range.To);
        }

        public override string ToString()
        {
            return $"{this.From} - {this.To}";
        }
    }
}
