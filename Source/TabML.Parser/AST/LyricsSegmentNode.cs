using System.Collections.Generic;
using TabML.Core;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Core.Parsing;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class LyricsSegmentNode : Node
    {
        public LiteralNode<string> Text { get; set; }

        public override IEnumerable<Node> Children
        {
            get { yield return this.Text; }
        }

        public LyricsSegmentNode()
        {

        }

        public LyricsSegmentNode(string lyricsSegment, TextRange range)
        {
            this.Text = new LiteralNode<string>(lyricsSegment, range);
            this.Range = range;
        }


        public bool ToDocumentElement(TablatureContext context, ILogger logger, out LyricsSegment lyricsSegment)
        {
            lyricsSegment = new LyricsSegment
            {
                Text = this.Text.Value,
                Range = this.Range
            };

            return true;
        }
    }

}
