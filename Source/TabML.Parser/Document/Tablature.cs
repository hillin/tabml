using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    public class Tablature
    {
        public Bar[] Bars { get; }

        internal Tablature(TablatureContext tablatureContext)
        {
            this.Bars = tablatureContext.Bars.ToArray();
        }
    }
}
