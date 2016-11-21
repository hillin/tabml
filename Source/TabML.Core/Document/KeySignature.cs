using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class KeySignature : Element
    {
        public NoteName Key { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
