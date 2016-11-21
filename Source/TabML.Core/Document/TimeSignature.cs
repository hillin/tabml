using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class TimeSignature : Element
    {
        public Time Time { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
