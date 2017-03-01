using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace TabML.Core.Parsing
{
    [DebuggerDisplay("{Row}:{Column}")]
    public struct TextPointer : IComparable<TextPointer>
    {
        public static readonly TextPointer Zero = new TextPointer(0, 0);

        public int Row { get; set; }
        public int Column { get; set; }

        public TextPointer(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public TextPointer Offset(TextPointer @base)
        {
            return new TextPointer(@base.Row + this.Row, @base.Column + this.Column);
        }

        public TextPointer OffsetColumn(int columnOffset)
        {
            return new TextPointer(this.Row, this.Column + columnOffset);
        }

        public TextRange AsRange()
        {
            return new TextRange(this, this);
        }

        internal TextRange AsRange(TextSource source)
        {
            return new TextRange(this, this, source);
        }

        public TextRange AsRange(TextPointer to, TextSource source)
        {
            return new TextRange(this, to, source);
        }

        public int CompareTo(TextPointer other)
        {
            return this.Row == other.Row ? this.Column.CompareTo(other.Column) : this.Row.CompareTo(other.Row);
        }

        public override string ToString()
        {
            return $"{this.Row}:{this.Column}";
        }

        public static bool operator ==(TextPointer p1, TextPointer p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(TextPointer p1, TextPointer p2)
        {
            return !p1.Equals(p2);
        }

        public static bool operator >(TextPointer p1, TextPointer p2)
        {
            return p1.CompareTo(p2) > 0;
        }

        public static bool operator <(TextPointer p1, TextPointer p2)
        {
            return p1.CompareTo(p2) < 0;
        }

        public static bool operator >=(TextPointer p1, TextPointer p2)
        {
            return p1.CompareTo(p2) >= 0;
        }

        public static bool operator <=(TextPointer p1, TextPointer p2)
        {
            return p1.CompareTo(p2) <= 0;
        }

        public bool Equals(TextPointer other)
        {
            return this.Row == other.Row && this.Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj)) return false;
            return obj is TextPointer && this.Equals((TextPointer)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Row * 397) ^ this.Column;
            }
        }



    }
}