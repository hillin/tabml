using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Parser.AST;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser
{
    public class TabMLParser
    {
        private class DummyReporter : ILogger
        {
            public void Report(LogLevel level, TextRange? position, string message, params object[] args)
            {
                Debug.WriteLine($"[{level}] [{position}] {string.Format(message, args)}");
                if (level == LogLevel.Error)
                {
                    
                }
            }
        }

        public static Tablature TryParse(string tabml)
        {
            TablatureNode tablatureNode;
            if (!new TablatureParser().TryParse(new Scanner(tabml), out tablatureNode))
                return null;

            var context = new TablatureContext();
            var logger = new DummyReporter();
            foreach (var node in tablatureNode.Nodes)
                node.Apply(context, logger);

            return context.ToTablature();
        }



    }
}
