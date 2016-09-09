using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing
{
    public struct TextRange
    {
        public TextPointer From { get; }
        public TextPointer To { get; }

        public TextRange(TextPointer from, TextPointer to)
        {
            this.From = @from;
            this.To = to;
        }

        public TextRange(TextPointer position)
            : this(position, position)
        {

        }
    }
}
