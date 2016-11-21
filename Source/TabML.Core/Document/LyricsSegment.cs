using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class LyricsSegment : Element
    {
        public string Text { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }
    }
}
