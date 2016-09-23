using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class RhythmTemplate
    {
        public List<RhythmTemplateSegment> Segments { get; }

        public RhythmTemplate()
        {
            this.Segments = new List<RhythmTemplateSegment>();
        }
    }
}
