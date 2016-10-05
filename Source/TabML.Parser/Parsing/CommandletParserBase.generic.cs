using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    abstract class CommandletParserBase<TCommandlet> : CommandletParserBase
        where TCommandlet : CommandletNode
    {
        public sealed override bool TryParse(Scanner scanner, out CommandletNode result)
        {
            TCommandlet commandlet;
            var success = this.TryParse(scanner, out commandlet);
            result = commandlet;
            if (commandlet != null)
            {
                commandlet.CommandletNameNode = this.CommandletNameNode;
                commandlet.Range = new TextRange(this.CommandletNameNode.Range.From,
                                                 scanner.LastReadRange.To,
                                                 scanner);
            }
            return success;
        }

        public abstract bool TryParse(Scanner scanner, out TCommandlet commandlet);
    }
}
