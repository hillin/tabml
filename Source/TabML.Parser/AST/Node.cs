using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    [DebuggerDisplay("{GetType().Name, nq}: {Range.Content}")]
    [DebuggerTypeProxy(typeof(DebugView))]
    public abstract class Node
    {
        internal class DebugView
        {
            private readonly Node _node;
#if DEBUG
            public string Source => _node.Range.Content;
#endif
            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public Node[] Children => _node.Children.ToArray();

            public DebugView(Node node)
            {
                _node = node;
            }
        }

        protected virtual string DebuggerDisplay => $"{this.GetType().Name}: {this.Range.Content}";

        private TextRange _range;

        public TextRange Range
        {
            get { return _range; }
            set { _range = value; }
        }

        public abstract IEnumerable<Node> Children { get; }

        public void SetRangeFrom(TextPointer from)
        {
            _range.From = from;
        }

        public void SetRangeTo(TextPointer to)
        {
            _range.To = to;
        }

        internal virtual bool Apply(TablatureContext context, ILogger logger)
        {
            return true;
        }
    }
}
