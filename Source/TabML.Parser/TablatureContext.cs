using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core;
using TabML.Parser.AST;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Parser
{
    class TablatureContext
    {
        public DocumentState DocumentState { get; private set; }

        private readonly List<Bar> _bars = new List<Bar>();
        public IReadOnlyList<Bar> Bars => _bars;

        public Bar CurrentBar { get; set; }
        public RhythmSegmentVoice CurrentVoice { get; set; }

        public TablatureContext()
        {
            this.DocumentState = new DocumentState();
        }

        public void AddBar(Bar bar)
        {
            if (!this.DocumentState.BarAppeared)
            {
                using (var state = this.AlterDocumentState())
                    state.BarAppeared = true;
            }

            bar.Index = _bars.Count;
            bar.DocumentState = this.DocumentState;
            bar.LogicalPreviousBar = _bars.LastOrDefault(); // todo: handle alternation

            _bars.Add(bar);
        }

        public EditableDocumentState AlterDocumentState()
        {
            var state = new EditableDocumentState(this.DocumentState);
            this.DocumentState = state;
            return state;
        }

        public Tablature ToTablature()
        {
            return new Tablature
            {
                Bars = this.Bars.ToArray()
            };
        }

        public BeatNote GetLastNoteOnString(int stringIndex)
        {
            var note = this.CurrentVoice.LastNoteOnStrings[stringIndex];
            if (note != null)
                return note;

            if (this.CurrentBar.OpenLine != OpenBarLine.BeginRepeat)
            {
                var bar = this.Bars.LastOrDefault();
                while (bar != null)
                {
                    if (bar.CloseLine == CloseBarLine.End
                        || bar.CloseLine == CloseBarLine.EndRepeat)
                        break;

                    if (bar.OpenLine == OpenBarLine.BeginRepeat)
                        break;

                    var voice = bar.GetVoice(this.CurrentVoice.Part);

                    if (voice.IsTerminatedWithRest)
                        break;

                    note = voice.LastNoteOnStrings[stringIndex];

                    if (note != null)
                    {
                        return note;
                    }

                    bar = bar.LogicalPreviousBar;
                }
            }

            return null;
        }

    }
}
