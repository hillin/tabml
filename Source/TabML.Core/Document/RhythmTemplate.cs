using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
