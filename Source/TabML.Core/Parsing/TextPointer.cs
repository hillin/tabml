using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing
{
    public struct TextPointer
    {
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
        
    }
}