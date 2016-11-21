using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class Lyrics : Element
    {
        public List<LyricsSegment> Segments { get; }
        public override IEnumerable<Element> Children => this.Segments;

        public Lyrics()
        {
            this.Segments = new List<LyricsSegment>();
        }
    }
}
