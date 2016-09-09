using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing
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
