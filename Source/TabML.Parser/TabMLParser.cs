using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.AST;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser
{
    public class TabMLParser
    {
        private class DummyReporter : IReporter
        {
            public void Report(ReportLevel level, TextRange? position, string message, params object[] args)
            {

            }
        }

        public static Tablature TryParse(string tabml)
        {
            TablatureNode tablatureNode;
            if (!new TablatureParser().TryParse(new Scanner(tabml), out tablatureNode))
                return null;

            var context = new TablatureContext();
            var reporter = new DummyReporter();
            foreach (var node in tablatureNode.Nodes)
                node.Apply(context, reporter);

            return context.ToTablature();
        }

    }
}
