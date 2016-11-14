using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.Document;
using TabML.Parser.Document;
using TabML.Parser.Parsing;
using DocumentChord = TabML.Core.Document.Chord;
using TheoreticalChord = TabML.Core.MusicTheory.Chord;

namespace TabML.Parser.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase
    {
        public LiteralNode<string> ChordName { get; set; }
        public LiteralNode<Chord> Chord { get; set; }
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

            if (this.Fingering != null)
            {
                ChordFingering chordFingering;
                if (!this.Fingering.ToDocumentElement(context, logger, out chordFingering))
                    return false;

                rhythmSegment.Chord = new DocumentChord
                {
                    Name = this.ChordName?.Value,
                    Fingering = chordFingering,
                    Range = this.ChordName?.Range.Union(this.Fingering.Range) ?? this.Fingering.Range
                };
            }
            else if (this.ChordName != null)
            {
                ChordFingering fingering;
                TheoreticalChord theoreticalChord;
                if (!context.DocumentState.LookupChord(this.ChordName.Value, out fingering, out theoreticalChord))
                {
                    logger.Report(LogLevel.Suggestion, this.ChordName.Range, Messages.Suggestion_UnknownChord, this.ChordName.Value);
                }

                rhythmSegment.Chord = new DocumentChord
                {
                    Name = this.ChordName.Value,
                    Fingering = fingering,
                    Range = this.ChordName.Range
                };
            }

            return true;
        }

    }
}
