using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;
using StrumTechniqueEnum = TabML.Core.MusicTheory.StrumTechnique;

namespace TabML.Parser.AST
{
    class BeatNode : Node, IValueEquatable<BeatNode>, IDocumentElementFactory<Beat>
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
                                                   || this.AllStringStrumTechnique != null
                                                   || this.StrumTechnique != null
                                                   || this.EffectTechnique != null
                                                   || this.EffectTechniqueParameter != null
                                                   || this.DurationEffect != null
                                                   || this.Accent != null;

        public bool HasRedunantSpecifierForTied => this.Rest != null
                                                   || this.AllStringStrumTechnique != null
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
                StrumTechnique = this.StrumTechnique?.Value ?? ((StrumTechniqueEnum?)this.AllStringStrumTechnique?.Value) ?? StrumTechniqueEnum.None,
                Accent = this.Accent?.Value ?? NoteAccent.Normal,
                DurationEffect = this.DurationEffect?.Value ?? NoteDurationEffect.None,
                EffectTechnique = this.EffectTechnique?.Value ?? NoteEffectTechnique.None,
                EffectTechniqueParameter = this.EffectTechniqueParameter?.Value ?? default(double),
                IsRest = this.Rest != null,
                IsTied = this.Tied != null,
                NoteValue = this.NoteValue.ToNoteValue(),
            };

            var notes = new List<BeatNote>();
            foreach (var note in this.Notes)
            {
                BeatNote documentNote;
                if (!note.ToDocumentElement(context, reporter, out documentNote))
                    return false;

                notes.Add(documentNote);
            }

            beat.Notes = notes.ToArray();

            return true;
        }

        public bool ValueEquals(BeatNode other)
        {
            if (other == null)
                return false;

            if (!ValueEquatable.ValueEquals(this.AllStringStrumTechnique, other.AllStringStrumTechnique)
                || !ValueEquatable.ValueEquals(this.StrumTechnique, other.StrumTechnique)
                || !ValueEquatable.ValueEquals(this.NoteValue, other.NoteValue)
                || !ValueEquatable.ValueEquals(this.Rest, other.Rest)
                || !ValueEquatable.ValueEquals(this.Tied, other.Tied)
                || !ValueEquatable.ValueEquals(this.EffectTechnique, other.EffectTechnique)
                || !ValueEquatable.ValueEquals(this.EffectTechniqueParameter, other.EffectTechniqueParameter)
                || !ValueEquatable.ValueEquals(this.DurationEffect, other.DurationEffect)
                || !ValueEquatable.ValueEquals(this.Accent, other.Accent))
                return false;

            if (other.Notes.Count != this.Notes.Count)
                return false;

            return !this.Notes.Where((n, i) => !n.ValueEquals(other.Notes[i])).Any();
        }
    }
}
