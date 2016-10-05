using System.Collections.Generic;
using TabML.Core.Document;

namespace TabML.Parser.AST
{
    class StaffCommandletNode : CommandletNode
    {
        public LiteralNode<string> StaffName { get; set; }
        public LiteralNode<StaffType> StaffType { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                if (this.StaffName != null)
                    yield return this.StaffName;

                if (this.StaffType != null)
                    yield return this.StaffType;
            }
        }
    }
}
