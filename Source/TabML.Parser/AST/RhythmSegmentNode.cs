using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;
using DocumentChord = TabML.Parser.Document.Chord;
using TheoreticalChord = TabML.Core.MusicTheory.Chord;

namespace TabML.Parser.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase, IDocumentElementFactory<RhythmSegment>
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

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out RhythmSegment rhythmSegment)
        {
            rhythmSegment = new RhythmSegment
            {
                Range = this.Range
            };

            if (!this.FillRhythmSegmentVoices(context, reporter, rhythmSegment))
                return false;

            if (this.Fingering != null)
            {
                rhythmSegment.Chord = new DocumentChord
                {
                    Name = this.ChordName?.Value,
                    Fingering = this.Fingering.GetFingeringIndices(),
                    Range = this.ChordName?.Range.Union(this.Fingering.Range) ?? this.Fingering.Range
                };
            }
            else if (this.ChordName != null)
            {
                int[] fingeringIndices;
                TheoreticalChord theoreticalChord;
                if (!context.DocumentState.LookupChord(this.ChordName.Value, out fingeringIndices, out theoreticalChord))
                {
                    reporter.Report(ReportLevel.Suggestion, this.ChordName.Range, Messages.Suggestion_UnknownChord, this.ChordName.Value);
                }

                rhythmSegment.Chord = new DocumentChord
                {
                    Name = this.ChordName.Value,
                    Fingering = fingeringIndices,
                    Range = this.ChordName.Range
                };
            }

            return true;
        }
        
    }
}
