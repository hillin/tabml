using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TabML.Parser.AST
{
    [DebuggerDisplay("instruction: {Range.Content}")]
    abstract class CommandletNode : TopLevelNode
    {
        public LiteralNode<string> CommandletNameNode { get; set; }

        public sealed override IEnumerable<Node> Children
        {
            get
            {
                yield return this.CommandletNameNode;
                foreach (var node in this.CommandletChildNodes)
                    yield return node;
            }
        }

        protected abstract IEnumerable<Node> CommandletChildNodes { get; }
    }
}
