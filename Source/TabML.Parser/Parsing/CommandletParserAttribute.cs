using System;

namespace TabML.Parser.Parsing
{
    [AttributeUsage(AttributeTargets.Class)]
    class CommandletParserAttribute : Attribute
    {
        public string CommandletName { get; }

        public CommandletParserAttribute(string commandletName)
        {
            this.CommandletName = commandletName;
        }

    }
}
