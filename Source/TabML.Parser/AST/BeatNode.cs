using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Parser.Parsing;
using AllStringStrumTechniqueEnum = TabML.Core.MusicTheory.AllStringStrumTechnique;
using StrumTechniqueEnum = TabML.Core.MusicTheory.StrumTechnique;

namespace TabML.Parser.AST
{
    class BeatNode : Node, IDocumentElementFactory<Beat>
    {
        public NoteValueNode NoteValue { get; set; }
        public ExistencyNode Rest { get; set; }
        public ExistencyNode Tied { get; set; }
        public LiteralNode<AllStringStrumTechniqueEnum> AllStringStrumTechnique { get; set; }
        public List<BeatNoteNode> Notes { get; }
        public LiteralNode<StrumTechniqueEnum> StrumTechnique { get; set; }
        public LiteralNode<BeatEffectTechnique> EffectTechnique { get; set; }
        public LiteralNode<double> EffectTechniqueParameter { get; set; }
        public LiteralNode<BeatDurationEffect> DurationEffect { get; set; }
        public LiteralNode<BeatAccent> Accent { get; set; }
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

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out Beat beat)
        {
            beat = new Beat
            {
                Range = this.Range,
                StrumTechnique = this.StrumTechnique?.Value ?? ((StrumTechniqueEnum?)this.AllStringStrumTechnique?.Value) ?? StrumTechniqueEnum.None,
                Accent = this.Accent?.Value ?? BeatAccent.Normal,
                DurationEffect = this.DurationEffect?.Value ?? BeatDurationEffect.None,
                EffectTechnique = this.EffectTechnique?.Value ?? BeatEffectTechnique.None,
                EffectTechniqueParameter = this.EffectTechniqueParameter?.Value ?? default(double),
                IsRest = this.Rest != null,
                IsTied = this.Tied != null,
                NoteValue = this.NoteValue.ToNoteValue(),
            };

            var notes = new List<BeatNote>();
            foreach (var note in this.Notes)
            {
                BeatNote documentNote;
                if (!note.ToDocumentElement(context, logger, out documentNote))
                    return false;

                notes.Add(documentNote);
            }

            beat.Notes = notes.ToArray();

            return true;
        }


        public bool ValueEquals(Beat other)
        {
            if (other == null)
                return false;

            if (this.AllStringStrumTechnique == null)
            {
                if ((this.StrumTechnique?.Value ?? StrumTechniqueEnum.None) != other.StrumTechnique)
                    return false;
            }
            else
            {
                if ((StrumTechniqueEnum)(this.AllStringStrumTechnique?.Value ?? AllStringStrumTechniqueEnum.None) != other.StrumTechnique)
                    return false;
            }

            if (this.NoteValue.ToNoteValue() != other.NoteValue)
                return false;

            if ((this.Rest != null) != other.IsRest)
                return false;

            if ((this.Tied != null) != other.IsTied)
                return false;

            if ((this.EffectTechnique?.Value ?? BeatEffectTechnique.None) != other.EffectTechnique)
                return false;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if ((this.EffectTechniqueParameter?.Value ?? default(double)) != other.EffectTechniqueParameter)
                return false;

            if ((this.DurationEffect?.Value ?? BeatDurationEffect.None) != other.DurationEffect)
                return false;

            if ((this.Accent?.Value ?? BeatAccent.Normal) != other.Accent)
                return false;

            if (other.Notes.Length != this.Notes.Count)
                return false;

            return !this.Notes.Where((n, i) => !n.ValueEquals(other.Notes[i])).Any();
        }
    }
}
