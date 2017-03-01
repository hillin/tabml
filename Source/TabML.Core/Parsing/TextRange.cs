using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TabML.Core.Parsing
{
    [DebuggerDisplay("{From} - {To}")]
    [DebuggerTypeProxy(typeof(TextRange.DebugView))]
    public struct TextRange : IEquatable<TextRange>
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

        internal TextRange(TextPointer from, int length, TextSource source)
            : this(from, from.OffsetColumn(length), source)
        {

        }

        internal TextRange(TextRange readRange, Capture capture, TextSource source)
            : this(readRange.From.OffsetColumn(capture.Index), capture.Length, source)
        {

        }

        public TextRange Union(TextRange range)
        {
            return new TextRange(this.From > range.From ? range.From : this.From,
                                 this.To > range.To ? this.To : range.To);
        }

        public bool Equals(TextRange other)
        {
            return this.From == other.From
                   && this.To == other.To
#if DEBUG
                   && this.Source == other.Source
#endif
                ;
        }

        public override bool Equals(object obj)
        {
            return obj is TextRange && this.Equals((TextRange)obj);
        }

        public static bool operator ==(TextRange r1, TextRange r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(TextRange r1, TextRange r2)
        {
            return !r1.Equals(r2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.From.GetHashCode();
                hashCode = (hashCode * 397) ^ this.To.GetHashCode();
#if DEBUG
                hashCode = (hashCode * 397) ^ (this.Source?.GetHashCode() ?? 0);
#endif
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{this.From} - {this.To}";
        }
    }
}
