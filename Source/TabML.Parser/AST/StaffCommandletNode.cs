using TabML.Core.Document;

namespace TabML.Parser.AST
{
    class StaffCommandletNode : CommandletNode
    {
        public LiteralNode<string> StaffName { get; set; }
        public LiteralNode<StaffType> StaffType { get; set; }
    }
}
