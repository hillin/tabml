using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    class ChordDefinition : Element
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int[] Fingering { get; set; }
    }
}
