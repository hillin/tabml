using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.Document;
using TabML.Parser.Parsing;
using DocumentChord = TabML.Core.Document.Chord;
using TheoreticalChord = TabML.Core.MusicTheory.Chord;

namespace TabML.Parser.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase
    {
        public LiteralNode<string> ChordName { get; set; }
        public ChordFingeringNode Fingering { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                foreach (var child in base.Children)
                    yield return child;

                if (this.ChordName != null)
                    yield return this.ChordName;

                if (this.Fingering != null)
                    yield return this.Fingering;
            }
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out RhythmSegment rhythmSegment)
        {
            rhythmSegment = new RhythmSegment
            {
                Range = this.Range
            };

            if (!this.FillRhythmSegmentVoices(context, logger, rhythmSegment))
                return false;

            ChordFingering chordFingering = null;

            if (this.ChordName != null || this.Fingering != null)
            {
                if (this.Fingering != null)
                {
                    if (!this.Fingering.ToDocumentElement(context, logger, out chordFingering))
                        return false;
                }

                var range = this.ChordName == null
                    // ReSharper disable once PossibleNullReferenceException
                    ? this.Fingering.Range
                    : this.Fingering == null
                        ? this.ChordName.Range
                        : this.ChordName.Range.Union(this.Fingering.Range);

                rhythmSegment.Chord = new DocumentChord
                {
                    Name = this.ChordName?.Value,
                    Fingering = chordFingering,
                    Range = range
                };
            }

            return true;
        }

    }
}
