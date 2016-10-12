using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature
{
    partial class Bar
    {
        private class ArrangedBeatNote
        {
            public int Fret { get; set; }
            public int StringIndex { get; set; }
        }

        private class ArrangedBeatVoice
        {
            public List<ArrangedBeatNote> Notes { get; }
            public NoteValue NoteValue { get; set; }
            public bool IsRest { get; set; }
            public bool IsTied { get; set; }
            public StrumTechnique StrumTechnique { get; set; } = StrumTechnique.None;
            public NoteEffectTechnique EffectTechnique { get; set; } = NoteEffectTechnique.None;
            public double EffectTechniqueParameter { get; set; }
            public NoteDurationEffect DurationEffect { get; set; } = NoteDurationEffect.None;
            public NoteAccent Accent { get; set; } = NoteAccent.Normal;

            public ArrangedBeatVoice()
            {
                this.Notes = new List<ArrangedBeatNote>();
            }
        }

        private class ArrangedBeat
        {
            public ArrangedBeatVoice RegularVoice { get; set; }
            public ArrangedBeatVoice BassVoice { get; set; }
        }
    }
}
