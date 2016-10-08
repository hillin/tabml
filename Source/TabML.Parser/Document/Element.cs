using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    public abstract class Element
    {
        public TextRange? Range { get; set; }
    }
}
