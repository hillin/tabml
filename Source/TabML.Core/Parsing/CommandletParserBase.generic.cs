﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    abstract class CommandletParserBase<TCommandlet> : CommandletParserBase
        where TCommandlet : CommandletNode
    {
        public sealed override bool TryParse(Scanner scanner, out CommandletNode result)
        {
            TCommandlet commandlet;
            var success = this.TryParse(scanner, out commandlet);
            result = commandlet;
            return success;
        }

        public abstract bool TryParse(Scanner scanner, out TCommandlet commandlet);
    }
}
