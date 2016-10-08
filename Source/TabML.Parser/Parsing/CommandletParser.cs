using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class CommandletParser
    {
        private static readonly Dictionary<string, Type> CommandletParsers;

        static CommandletParser()
        {
            CommandletParsers = new Dictionary<string, Type>();

            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes))
            {
                if (!(typeof(CommandletParserBase)).IsAssignableFrom(type))
                    continue;

                var attributes = type.GetCustomAttributes(typeof(CommandletParserAttribute), false);

                if (attributes.Length > 0)
                    CommandletParsers.Add(((CommandletParserAttribute)attributes[0]).CommandletName.ToLowerInvariant(), type);
            }
        }

        public static bool TryCreate(Scanner scanner, IReporter reporter, out CommandletParserBase parser)
        {
            var anchor = scanner.MakeAnchor();
            if (!scanner.Expect('+'))
            {
                reporter.Report(ReportLevel.Error, scanner.Pointer.AsRange(scanner), Messages.Error_InstructionExpected);
                parser = null;
                return false;
            }

            var name = scanner.Read(c => char.IsLetterOrDigit(c) || c == '-');
            Type parserType;
            if (!CommandletParsers.TryGetValue(name.ToLowerInvariant(), out parserType))
            {
                reporter.Report(ReportLevel.Error, scanner.Pointer.AsRange(scanner), Messages.Error_UnknownInstruction);
                parser = null;
                return false;
            }

            var nameNode = new LiteralNode<string>($"+{name}", anchor.Range);

            parser = (CommandletParserBase)Activator.CreateInstance(parserType);
            parser.CommandletNameNode = nameNode;
            return true;
        }


    }
}
