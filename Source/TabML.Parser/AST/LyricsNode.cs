using System.Collections.Generic;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class LyricsNode : Node
    {
        public List<LyricsSegmentNode> LyricsSegments { get; }

        public LyricsNode()
        {
            this.LyricsSegments = new List<LyricsSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.LyricsSegments;

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Lyrics lyrics)
        {
            lyrics = new Lyrics
            {
                Range = this.Range
            };

            foreach (var segment in this.LyricsSegments)
            {
                LyricsSegment lyricsSegment;
                if (!segment.ToDocumentElement(context, reporter, out lyricsSegment))
                    return false;

                lyrics.Segments.Add(lyricsSegment);
            }

            return true;
        }
    }
}
