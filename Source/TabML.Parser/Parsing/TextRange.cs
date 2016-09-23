using System.Text.RegularExpressions;

namespace TabML.Parser.Parsing
{
    public struct TextRange
    {
        public TextPointer From { get; set; }
        public TextPointer To { get; set; }

        public TextRange(TextPointer from, TextPointer to)
        {
            this.From = @from;
            this.To = to;
        }

        public TextRange(TextPointer position)
            : this(position, position)
        {

        }

        public TextRange(TextPointer from, int length)
            : this(from, new TextPointer(from.Row, from.Column + length))
        {

        }

        public TextRange(TextPointer @base, int offset, int length)
            : this(new TextPointer(@base.Row, @base.Column + offset), length)
        {

        }

        public TextRange(TextRange readRange, Capture capture)
            : this(readRange.From, capture.Index, capture.Length)
        {

        }

        public TextRange Offset(TextPointer @base)
        {
            return new TextRange(this.From.Offset(@base), this.To.Offset(@base));
        }
    }
}
