using System.Collections.Generic;
using TabML.Parser.Document;
using TabML.Parser.Parsing;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class VoiceNode : Node
    {
        public List<BeatNode> Beats { get; }

        public override IEnumerable<Node> Children => this.Beats;

        public VoiceNode()
        {
            this.Beats = new List<BeatNode>();
        }


        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Voice voice)
        {
            voice = new Voice()
            {
                Range = this.Range
            };

            //var timeSignature = context.DocumentState.Time;
            //var sumExplicitBeats = this.Beats.Sum(beat => beat.NoteValue.ToNoteValue().GetBeats(timeSignature.NoteValue.Value));

            //if (timeSignature.Beats.Value < sumExplicitBeats)
            //{

            //}

            foreach (var beat in this.Beats)
            {
                Beat documentBeat;
                if (!beat.ToDocumentElement(context, reporter, out documentBeat))
                    return false;

                voice.Beats.Add(documentBeat);
            }

            return true;
        }
    }
}
