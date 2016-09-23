using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class RhythmUnitNode : Node, IRequireStringValidation
    {
        public NoteValueNode NoteValue { get; set; }
        public List<RhythmUnitNoteNode> Notes { get; }
        public LiteralNode<StrumTechnique> StrumTechnique { get; set; }
        public LiteralNode<NoteEffectTechnique> EffectTechnique { get; set; }
        public LiteralNode<double> EffectTechniqueParameter { get; set; }
        public LiteralNode<NoteDurationEffect> DurationEffect { get; set; }
        public LiteralNode<NoteAccent> Accent { get; set; }

        public RhythmUnitNode()
        {
            this.Notes = new List<RhythmUnitNoteNode>();
        }
    }
}
