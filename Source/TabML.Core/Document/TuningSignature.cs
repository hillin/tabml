using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class TuningSignature : Element
    {
        public Tuning Tuning { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
