using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class BeatNode : Node
    {
        public NoteValueNode NoteValue { get; set; }
        public RestNode Rest { get; set; }
        public TiedNode Tied { get; set; }
        public LiteralNode<AllStringStrumTechnique> AllStringStrumTechnique { get; set; }
        public List<BeatNoteNode> Notes { get; }
        public LiteralNode<StrumTechnique> StrumTechnique { get; set; }
        public LiteralNode<NoteEffectTechnique> EffectTechnique { get; set; }
        public LiteralNode<double> EffectTechniqueParameter { get; set; }
        public LiteralNode<NoteDurationEffect> DurationEffect { get; set; }
        public LiteralNode<NoteAccent> Accent { get; set; }
        public List<Node> Modifiers { get; }

        public bool HasRedunantSpecifierForRest => this.Notes.Count > 0
                                                   || this.StrumTechnique != null
                                                   || this.EffectTechnique != null
                                                   || this.EffectTechniqueParameter != null
                                                   || this.DurationEffect != null
                                                   || this.Accent != null;

        public bool HasRedunantSpecifierForTied => this.Rest != null
                                                   || this.Notes.Count > 0
                                                   || this.StrumTechnique != null
                                                   || this.EffectTechnique != null
                                                   || this.EffectTechniqueParameter != null
                                                   || this.DurationEffect != null
                                                   || this.Accent != null;

        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.Tied != null)
                    yield return this.Tied;

                yield return this.NoteValue;

                if (this.Rest != null)
                    yield return this.Rest;

                if (this.AllStringStrumTechnique != null)
                    yield return this.AllStringStrumTechnique;

                foreach (var node in this.Notes)
                    yield return node;

                foreach (var node in this.Modifiers)
                    yield return node;
            }
        }

        public BeatNode()
        {
            this.Notes = new List<BeatNoteNode>();
            this.Modifiers = new List<Node>();
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Beat beat)
        {
            beat = new Beat
            {
                Range = this.Range,
                StrumTechnique = this.StrumTechnique?.Value ?? Core.MusicTheory.StrumTechnique.None,
                Accent = this.Accent?.Value ?? NoteAccent.Normal,
                DurationEffect = this.DurationEffect?.Value ?? NoteDurationEffect.None,
                EffectTechnique = this.EffectTechnique?.Value ?? NoteEffectTechnique.None,
                EffectTechniqueParameter = this.EffectTechniqueParameter?.Value ?? default(double),
                IsRest = this.Rest != null,
                IsTied = this.Tied != null,
                NoteValue = this.NoteValue.ToNoteValue(),
            };
        }
    }
}
