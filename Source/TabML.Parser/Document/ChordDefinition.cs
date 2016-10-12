using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    public class ChordDefinition : Element
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ChordFingering Fingering { get; set; }

        public string GetDisplayName()
        {
            return !string.IsNullOrEmpty(this.DisplayName) ? this.DisplayName : this.Name;
        }
    }
}
