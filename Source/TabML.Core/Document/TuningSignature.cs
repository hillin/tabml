using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Core.String;

namespace TabML.Core.Document
{
    public class TuningSignature : Element
    {
        public Tuning Tuning { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
