using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class Tablature : Element
    {
        public Bar[] Bars { get; set; }
        public override IEnumerable<Element> Children => this.Bars;
    }
}
