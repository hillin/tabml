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

        public static CommandletParserBase Create(string commandletName)
        {
            Type parserType;
            if (!CommandletParsers.TryGetValue(commandletName.ToLowerInvariant(), out parserType))
                return null;

            return (CommandletParserBase)Activator.CreateInstance(parserType);
        }

        public static CommandletParserBase Create(Scanner scanner)
        {
            if (!scanner.Expect('+'))
                return null;

            var name = scanner.Read(c => char.IsLetterOrDigit(c) || c == '-');
            return CommandletParser.Create(name);
        }


    }
}
