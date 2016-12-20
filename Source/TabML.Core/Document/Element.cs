using System.Collections.Generic;

namespace TabML.Core.Document
{
    public abstract class Element : ElementBase
    {
        public abstract IEnumerable<Element> Children { get; }

        public TextRange? Range { get; set; }
    }
}
 