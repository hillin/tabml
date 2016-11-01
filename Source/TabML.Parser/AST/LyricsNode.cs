using System.Collections.Generic;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class LyricsNode : Node, IDocumentElementFactory<Lyrics>
    {
        public List<LyricsSegmentNode> LyricsSegments { get; }

        public LyricsNode()
        {
            this.LyricsSegments = new List<LyricsSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.LyricsSegments;

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out Lyrics lyrics)
        {
            lyrics = new Lyrics
            {
                Range = this.Range
            };

            foreach (var segment in this.LyricsSegments)
            {
                LyricsSegment lyricsSegment;
                if (!segment.ToDocumentElement(context, logger, out lyricsSegment))
                    return false;

                lyrics.Segments.Add(lyricsSegment);
            }

            return true;
        }
    }
}
