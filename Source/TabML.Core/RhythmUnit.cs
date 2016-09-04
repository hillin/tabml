using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class RhythmUnit
    {
        public NoteValue NoteValue { get; set; }
        public int[] Strings { get; set; }
        public StrumTechnique StrumTechnique { get; set; } = StrumTechnique.None;
        public NoteConnection UnitConnection { get; set; } = NoteConnection.None;
        public NoteEffectTechnique EffectTechnique { get; set; } = NoteEffectTechnique.None;
        public double EffectTechniqueParameter { get; set; }
        public NoteDurationEffect DurationEffect { get; set; } = NoteDurationEffect.None;
        public NoteAccent Accent { get; set; } = NoteAccent.Normal;
        

        public double GetDuration()
        {
            return this.NoteValue.GetDuration();
        }
    }
}
