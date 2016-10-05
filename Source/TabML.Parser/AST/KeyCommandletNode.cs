using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class KeyCommandletNode : CommandletNode
    {
        public NoteNameNode Key { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.Key;
            }
        }
    }
}
