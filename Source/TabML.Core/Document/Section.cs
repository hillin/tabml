using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class Section : Element
    {
        public string Name { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
