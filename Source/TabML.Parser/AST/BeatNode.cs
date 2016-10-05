using System.Collections.Generic;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class BeatNode : Node, IRequireStringValidation
    {
        public NoteValueNode NoteValue { get; set; }
        public RestNode Rest { get; set; }
        public TiedNode Tied { get; set; }
        public LiteralNode<AllStringStrumTechnique> AllStringStrumTechnique { get; set; }
        public List<RhythmUnitNoteNode> Notes { get; }
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
                    yield return this.NoteValue;

                if (this.NoteValue != null)
                    yield return this.NoteValue;

                if (this.Rest != null)
                    yield return this.NoteValue;

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
            this.Notes = new List<RhythmUnitNoteNode>();
            this.Modifiers = new List<Node>();
        }
    }
}
