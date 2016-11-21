using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class TempoSignature : Element
    {
        public Tempo Tempo { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
