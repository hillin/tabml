using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    class CommandletParser
    {
        private static readonly Dictionary<string, Type> CommandletParsers;

        static CommandletParser()
        {
            CommandletParsers = new Dictionary<string, Type>();

            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.ExportedTypes))
            {
                if (!type.IsSubclassOf(typeof(CommandletParserBase)))
                    continue;

                var attributes = type.GetCustomAttributes(typeof(CommandletParserAttribute), false);

                if (attributes.Length > 0)
                    CommandletParsers.Add(((CommandletParserAttribute)attributes[0]).CommandletName.ToLowerInvariant(), type);
            }
        }

        public static CommandletParserBase Create(Scanner scanner)
        {
            var anchor = scanner.MakeAnchor();
            if (!scanner.Expect('+'))
                return null;

            var name = scanner.Read(c => char.IsLetterOrDigit(c) || c == '-');
            Type parserType;
            if (!CommandletParsers.TryGetValue(name.ToLowerInvariant(), out parserType))
                return null;

            var nameNode = new StringNode($"+{name}", anchor.Range);

            var parser = (CommandletParserBase)Activator.CreateInstance(parserType);
            parser.CommandletNameNode = nameNode;
            return parser;
        }


    }
}
