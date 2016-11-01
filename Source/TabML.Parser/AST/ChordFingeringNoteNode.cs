using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core;
using TabML.Core.Document;
using TabML.Parser.Parsing.Bar;

namespace TabML.Parser.AST
{
    class ChordFingeringNoteNode : Node
    {
        public LiteralNode<int> Fret { get; set; }
        public LiteralNode<LeftHandFingerIndex> FingerIndex { get; set; }
        public ExistencyNode Importancy { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                yield return this.Fret;
                if (this.FingerIndex != null)
                    yield return this.FingerIndex;
                if (this.Importancy != null)
                    yield return this.Importancy;
            }
        }

        public ChordFingeringNote ToDocumentElement(bool ignoreFingerIndex)
        {
            return new ChordFingeringNote
            {
                Range = this.Range,
                Fret = this.Fret.Value,
                FingerIndex = ignoreFingerIndex ? null : this.FingerIndex?.Value,
                IsImportant = this.Importancy != null
            };
        }
    }
}
