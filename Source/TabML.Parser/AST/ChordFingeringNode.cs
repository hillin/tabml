using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class ChordFingeringNode : Node, IDocumentElementFactory<ChordFingering>
    {
        public const int FingeringSkipString = -1;

        public List<ChordFingeringNoteNode> Fingerings { get; }

        public ChordFingeringNode()
        {
            this.Fingerings = new List<ChordFingeringNoteNode>();
        }

        public override IEnumerable<Node> Children => this.Fingerings;


        public bool ToDocumentElement(TablatureContext context, ILogger logger, out ChordFingering element)
        {
            bool? fingerIndexSpecified = null;
            var ignoreFingerIndices = false;
            foreach (var fingering in this.Fingerings)
            {
                if (fingering.Fret.Value == FingeringSkipString || fingering.Fret.Value == 0)
                    continue;

                if (fingerIndexSpecified == null)
                {
                    fingerIndexSpecified = fingering.FingerIndex != null;
                    continue;
                }

                if ((fingering.FingerIndex != null && fingerIndexSpecified == false)
                    || (fingering.FingerIndex == null && fingerIndexSpecified == true))
                {
                    logger.Report(LogLevel.Warning, this.Range, Messages.Warning_ChordNotAllFingerIndexSpecified);
                    ignoreFingerIndices = true;
                    break;
                }
            }

            // todo: validate unreasonable fingering

            element = new ChordFingering
            {
                Range = this.Range,
                Notes = this.Fingerings.Select(f => f.ToDocumentElement(ignoreFingerIndices)).ToArray()
            };

            return true;
        }
    }
}
