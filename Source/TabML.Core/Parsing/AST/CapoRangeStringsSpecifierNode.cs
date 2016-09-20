using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class CapoRangeStringsSpecifierNode : CapoStringsSpecifierNode
    {
        public IntegerNode From { get; set; }
        public IntegerNode To { get; set; }
        public override int[] GetStringNumbers()
        {
            return
                Enumerable.Range(Math.Min(this.From.Value, this.To.Value), Math.Abs(this.To.Value - this.From.Value))
                          .ToArray();
        }
    }
}
