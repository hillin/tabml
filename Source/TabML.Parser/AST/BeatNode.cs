using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Core.String;
using TabML.Core.Style;
using TabML.Parser.Parsing;
using ChordStrumTechniqueEnum = TabML.Core.String.Plucked.ChordStrumTechnique;
using StrumTechniqueEnum = TabML.Core.String.Plucked.StrumTechnique;

namespace TabML.Parser.AST
{
    class BeatNode : Node
    {
        public NoteValueNode NoteValue { get; set; }
        public ExistencyNode ForceBeamStart { get; set; } // todo: handle force beam
        public ExistencyNode ForceBeamEnd { get; set; }
        public ExistencyNode Rest { get; set; }
        public ExistencyNode Tie { get; set; }
        public LiteralNode<VerticalDirection> TiePosition { get; set; }
        public LiteralNode<PreBeatConnection> PreConnection { get; set; }
        public LiteralNode<PostBeatConnection> PostConnection { get; set; }
        public LiteralNode<ChordStrumTechniqueEnum> ChordStrumTechnique { get; set; }
        public List<BeatNoteNode> Notes { get; }
        public LiteralNode<StrumTechniqueEnum> StrumTechnique { get; set; }
        public LiteralNode<Ornament> Ornament { get; set; }
        public LiteralNode<NoteRepetition> NoteRepetition { get; set; }
        public LiteralNode<double> OrnamentParameter { get; set; }
        public LiteralNode<HoldAndPause> HoldAndPause { get; set; }
        public LiteralNode<BeatAccent> Accent { get; set; }
        public List<Node> Modifiers { get; }

        public bool HasRedunantSpecifierForRest => this.Notes.Count > 0
                                                   || this.Tie != null
                                                   || this.PreConnection != null
                                                   || this.PostConnection != null
                                                   || this.ChordStrumTechnique != null
                                                   || this.StrumTechnique != null
                                                   || this.Ornament != null
                                                   || this.OrnamentParameter != null
                                                   || this.NoteRepetition != null
                                                   || this.HoldAndPause != null
                                                   || this.Accent != null;

        public bool HasRedunantSpecifierForTied => this.Rest != null
                                                   || this.PreConnection != null
                                                   || this.ChordStrumTechnique != null
                                                   || this.Notes.Count > 0
                                                   || this.StrumTechnique != null
                                                   || this.Ornament != null
                                                   || this.OrnamentParameter != null
                                                   || this.NoteRepetition != null
                                                   || this.HoldAndPause != null
                                                   || this.Accent != null;

        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.ForceBeamStart != null)
                    yield return this.ForceBeamStart;

                if (this.PreConnection != null)
                {
                    yield return this.PreConnection;

                    if (this.TiePosition != null
                        && this.TiePosition.Range != this.PreConnection.Range)
                        yield return this.TiePosition;
                }

                yield return this.NoteValue;

                if (this.Rest != null)
                    yield return this.Rest;

                if (this.ChordStrumTechnique != null)
                    yield return this.ChordStrumTechnique;

                foreach (var node in this.Notes)
                    yield return node;

                foreach (var node in this.Modifiers)
                    yield return node;

                if (this.PostConnection != null)
                    yield return this.PostConnection;

                if (this.ForceBeamEnd != null)
                    yield return this.ForceBeamEnd;
            }
        }

        public BeatNode()
        {
            this.Notes = new List<BeatNoteNode>();
            this.Modifiers = new List<Node>();
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, RhythmSegmentVoice ownerVoice, out Beat beat)
        {
            beat = new Beat()
            {
                Range = this.Range,
                StrumTechnique = this.StrumTechnique?.Value ?? (StrumTechniqueEnum?)this.ChordStrumTechnique?.Value ?? StrumTechniqueEnum.None,
                Accent = this.Accent?.Value ?? Core.MusicTheory.BeatAccent.Normal,
                HoldAndPause = this.HoldAndPause?.Value ?? Core.MusicTheory.HoldAndPause.None,
                Ornament = this.Ornament?.Value ?? Core.MusicTheory.Ornament.None,
                NoteRepetition = this.NoteRepetition?.Value ?? Core.MusicTheory.NoteRepetition.None,
                EffectTechniqueParameter = this.OrnamentParameter?.Value ?? default(double),
                IsRest = this.Rest != null,
                IsTied = this.Tie != null,
                TiePosition = this.TiePosition?.Value,
                PreConnection = this.PreConnection?.Value ?? PreBeatConnection.None,
                PostConnection = this.PostConnection?.Value ?? PostBeatConnection.None,
                NoteValue = this.NoteValue.ToNoteValue(),
                VoicePart = ownerVoice.Part,
                IsForceBeamStart = this.ForceBeamStart != null,
                IsForceBeamEnd = this.ForceBeamEnd != null
            };

            if (!this.Validate(context, logger, beat))
            {
                return false;
            }

            var notes = new List<BeatNote>();
            foreach (var note in this.Notes)
            {
                BeatNote documentNote;
                if (!note.ToDocumentElement(context, logger, ownerVoice.Part, out documentNote))
                    return false;
                documentNote.OwnerBeat = beat;

                notes.Add(documentNote);

                ownerVoice.LastNoteOnStrings[documentNote.String] = documentNote;
            }

            beat.Notes = notes.ToArray();

            ownerVoice.IsTerminatedWithRest = beat.IsRest;

            return true;
        }

        private bool Validate(TablatureContext context, ILogger logger, Beat beat)
        {
            if (beat.StrumTechnique != StrumTechniqueEnum.None
                && this.StrumTechnique != null) // strum technique == null means we are derived from a template
            {
                if (beat.IsTied)
                {
                    logger.Report(LogLevel.Warning, this.StrumTechnique.Range,
                                  Messages.Warning_StrumTechniqueForTiedBeat);

                    beat.StrumTechnique = StrumTechniqueEnum.None;
                }
                else if (beat.IsRest)
                {
                    logger.Report(LogLevel.Warning, this.StrumTechnique.Range,
                                  Messages.Warning_StrumTechniqueForRestBeat);

                    beat.StrumTechnique = StrumTechniqueEnum.None;
                }
            }

            return true;
        }

        public bool ValueEquals(Beat other)
        {
            if (other == null)
                return false;

            if (this.ChordStrumTechnique == null)
            {
                if ((this.StrumTechnique?.Value ?? StrumTechniqueEnum.None) != other.StrumTechnique)
                    return false;
            }
            else
            {
                if ((StrumTechniqueEnum)(this.ChordStrumTechnique?.Value ?? ChordStrumTechniqueEnum.None) != other.StrumTechnique)
                    return false;
            }

            if (this.NoteValue.ToNoteValue() != other.NoteValue)
                return false;

            if ((this.Rest != null) != other.IsRest)
                return false;

            if ((this.Tie != null) != other.IsTied)
                return false;

            if (this.TiePosition?.Value != other.TiePosition)
                return false;

            if ((this.PreConnection?.Value ?? PreBeatConnection.None) != other.PreConnection)
                return false;

            if (this.PostConnection?.Value != other.PostConnection)
                return false;

            if ((this.Ornament?.Value ?? Core.MusicTheory.Ornament.None) != other.Ornament)
                return false;

            if ((this.NoteRepetition?.Value ?? Core.MusicTheory.NoteRepetition.None) != other.NoteRepetition)
                return false;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if ((this.OrnamentParameter?.Value ?? default(double)) != other.EffectTechniqueParameter)
                return false;

            if ((this.HoldAndPause?.Value ?? Core.MusicTheory.HoldAndPause.None) != other.HoldAndPause)
                return false;

            if ((this.Accent?.Value ?? Core.MusicTheory.BeatAccent.Normal) != other.Accent)
                return false;

            if (other.Notes.Length != this.Notes.Count)
                return false;

            return !this.Notes.Where((n, i) => !n.ValueEquals(other.Notes[i])).Any();
        }
    }
}
