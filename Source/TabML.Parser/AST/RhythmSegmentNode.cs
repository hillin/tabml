using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase, IValueEquatable<RhythmSegmentNode>
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

        public override bool ToDocumentElement(TablatureContext context, IReporter reporter, out RhythmSegment rhythmSegment)
        {
            if (!base.ToDocumentElement(context, reporter, out rhythmSegment))
                return false;

            if (this.Fingering != null)
            {
                rhythmSegment.Chord = new Chord
                {
                    Name = this.ChordName?.Value,
                    Fingering = this.Fingering.GetFingeringIndices(),
                    Range = this.ChordName?.Range.Union(this.Fingering.Range) ?? this.Fingering.Range
                };
            }
            else if (this.ChordName != null)
            {
                int[] fingeringIndices;
                if (!context.DocumentState.LookupChord(this.ChordName.Value, out fingeringIndices))
                {
                    reporter.Report(ReportLevel.Suggestion, this.ChordName.Range, Messages.Suggestion_UnknownChord, this.ChordName.Value);
                }

                rhythmSegment.Chord = new Chord
                {
                    Name = this.ChordName.Value,
                    Fingering = fingeringIndices,
                    Range = this.ChordName.Range
                };
            }

            return true;
        }

        public bool ValueEquals(RhythmSegmentNode other)
        {
            if (!base.ValueEquals(other))
                return false;

            return ValueEquatable.ValueEquals(this.ChordName, other.ChordName)
                   && ValueEquatable.ValueEquals(this.Fingering, other.Fingering);
        }
    }
}
