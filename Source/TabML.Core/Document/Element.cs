using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TabML.Core.Document
{
    [DebuggerDisplay("{GetType().Name, nq}: {Range?.Content}")]
    [DebuggerTypeProxy(typeof(DebugView))]

    public abstract class Element : ElementBase
    {
        internal class DebugView
        {
            private readonly Element _element;
#if DEBUG
            public string Source => _element.Range?.Content;
#endif
            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public Element[] Children => _element.Children.ToArray();

            public DebugView(Element element)
            {
                _element = element;
            }
        }
        public abstract IEnumerable<Element> Children { get; }

        public TextRange? Range { get; set; }
    }
}
 