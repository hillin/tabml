using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class SectionCommandletNode : CommandletNode
    {
        public string SectionName { get; }

        public SectionCommandletNode(string sectionName)
        {
            this.SectionName = sectionName;
        }
        
    }
}
