﻿using TabML.Core.Logging;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("capo")]
    class CapoCommandletParser : CommandletParserBase<CapoCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out CapoCommandletNode commandlet)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            commandlet = new CapoCommandletNode();

            scanner.SkipOptional(':', true);

            LiteralNode<int> positionNode;
            if (!Parser.TryReadInteger(scanner, out positionNode))
            {
                this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_InvalidCapoPosition);
                commandlet = null;
                return false;
            }

            if (positionNode.Value > 12)
            {
                this.Report(LogLevel.Warning, scanner.LastReadRange, Messages.Warning_CapoTooHigh);
            }

            commandlet.Position = positionNode;

            scanner.SkipWhitespaces();

            if (scanner.Peek() == '(')
            {
                CapoStringsSpecifierNode stringsSpecifierNode;
                if (!new CapoStringsSpecifierParser().TryParse(scanner, out stringsSpecifierNode))
                {
                    commandlet = null;
                    return false;
                }

                commandlet.StringsSpecifier = stringsSpecifierNode;
            }
            
            return true;

        }
        
        protected override CommandletNode Recover(Scanner scanner)
        {
            return null;
        }
    }
}
