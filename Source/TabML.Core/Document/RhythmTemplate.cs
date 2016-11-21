using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;

namespace TabML.Core.Document
{
    public class RhythmTemplate : Element
    {
        public List<RhythmTemplateSegment> Segments { get; }
        public override IEnumerable<Element> Children => this.Segments;

        public RhythmTemplate()
        {
            this.Segments = new List<RhythmTemplateSegment>();
        }

        public Rhythm Instantialize()
        {
            var rhythm = new Rhythm();  // do not set Range
            rhythm.Segments.AddRange(this.Segments.Select(s => s.Instantialize()));
            return rhythm;
        }
        
    }
}
