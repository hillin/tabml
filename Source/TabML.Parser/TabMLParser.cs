using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.AST;
using TabML.Parser.Parsing;

namespace TabML.Parser
{
    public class TabMLParser
    {
        public static void TryParse(string tabml)
        {
            TablatureNode tablature;
            new TablatureParser().TryParse(new Scanner(tabml), out tablature);
        }
    }
}
