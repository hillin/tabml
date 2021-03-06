﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Core.MusicTheory.String;
using TabML.Core.Style;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class BeatNoteNode : Node
    {
        public LiteralNode<int> String { get; set; }
        public LiteralNode<int> Fret { get; set; }
        public ExistencyNode Tie { get; set; }
        public LiteralNode<VerticalDirection> TiePosition { get; set; }
        public LiteralNode<PreNoteConnection> PreConnection { get; set; }
        public LiteralNode<PostNoteConnection> PostConnection { get; set; }
        public LiteralNode<NoteEffectTechnique> EffectTechnique { get; set; }
        public LiteralNode<double> EffectTechniqueParameter { get; set; }
        public LiteralNode<NoteAccent> Accent { get; set; }
        
        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.PreConnection != null)
                {
                    yield return this.PreConnection;
                    if (this.TiePosition != null
                        && this.TiePosition.Range != this.PreConnection.Range)
                        yield return this.TiePosition;
                }

                yield return this.String;

                if (this.Fret != null)
                    yield return this.Fret;

                if (this.EffectTechnique != null)
                {
                    yield return this.EffectTechnique;
                    if (this.EffectTechniqueParameter != null)
                        yield return this.EffectTechniqueParameter;
                }

                if (this.PostConnection != null)
                    yield return this.PostConnection;
            }
        }


        public bool ValueEquals(BeatNote other)
        {
            if (other == null)
                return false;

            if (this.String.Value - 1 != other.String)
                return false;

            if ((this.Fret?.Value ?? BeatNote.UnspecifiedFret) != other.Fret)
                return false;

            if ((this.EffectTechnique?.Value ?? NoteEffectTechnique.None) != other.EffectTechnique)
                return false;
            
            if (this.EffectTechniqueParameter?.Value != other.EffectTechniqueParameter)
                return false;

            if ((this.Accent?.Value ?? NoteAccent.Normal) != other.Accent)
                return false;

            if ((this.Tie != null) != other.IsTied)
                return false;

            if (this.TiePosition?.Value != other.TiePosition)
                return false;

            if ((this.PreConnection?.Value ?? PreNoteConnection.None) != other.PreConnection)
                return false;

            if ((this.PostConnection?.Value ?? PostNoteConnection.None) != other.PostConnection)
                return false;

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, VoicePart voicePart, out BeatNote element)
        {
            var documentState = context.DocumentState;
            if (this.Fret != null
                && this.Fret.Value + documentState.MinimumCapoFret < (documentState.CapoFretOffsets?[this.String.Value - 1] ?? 0))
            {
                logger.Report(LogLevel.Warning, this.Fret.Range,
                                Messages.Warning_FretUnderCapo, this.String.Value,
                                this.Fret.Value);
            }

            element = new BeatNote
            {
                Range = this.Range,
                PreConnection = this.PreConnection?.Value ?? PreNoteConnection.None,
                PostConnection = this.PostConnection?.Value ?? PostNoteConnection.None,
                IsTied = this.Tie != null,
                TiePosition = this.TiePosition?.Value,
                String = this.String.Value - 1,
                Fret = this.Fret?.Value ?? BeatNote.UnspecifiedFret,
                EffectTechnique = this.EffectTechnique?.Value ?? NoteEffectTechnique.None,
                EffectTechniqueParameter = this.EffectTechniqueParameter?.Value,
                Accent = this.Accent?.Value ?? NoteAccent.Normal
            };

            if (!this.Validate(context, logger, voicePart, element))
                return false;

            return true;
        }

        private bool Validate(TablatureContext context, ILogger logger, VoicePart voicePart, BeatNote element)
        {
            if (!this.ValidateTie(context, logger, element))
                return false;

            if (!this.ValidatePreConnection(context, logger, voicePart, element))
                return false;

            if (!this.ValidatePostConnection(context, logger, voicePart, element))
                return false;
            return true;
        }


        private bool ValidateTie(TablatureContext context, ILogger logger, BeatNote element)
        {
            if (this.Tie == null)
                return true;

            if (!this.RetrievePreConnectedNote(context, logger, element))
                return false;

            if (this.Fret == null)
                element.Fret = element.PreConnectedNote.Fret;
            else if (this.Fret.Value != element.PreConnectedNote.Fret)
            {
                logger.Report(LogLevel.Warning, this.Fret.Range,
                              Messages.Warning_TiedNoteMismatch);
                element.PreConnection = PreNoteConnection.None;
            }
            return true;
        }

        private bool RetrievePreConnectedNote(TablatureContext context, ILogger logger, BeatNote element)
        {
            var lastNote = context.GetLastNoteOnString(this.String.Value - 1);

            if (lastNote == null)
            {
                logger.Report(LogLevel.Error, this.PreConnection.Range,
                              Messages.Error_ConnectionPredecessorNotExisted);
                return false;
            }

            element.PreConnectedNote = lastNote;

            return true;
        }

        private bool ValidatePostConnection(TablatureContext context, ILogger logger, VoicePart voicePart, BeatNote element)
        {
            if (this.PostConnection == null || this.PostConnection.Value == PostNoteConnection.None)
                return true;

            if (this.PostConnection.Value == PostNoteConnection.SlideOutToHigher
                || this.PostConnection.Value == PostNoteConnection.SlideOutToLower)
            {
                if (this.Fret == null)
                {
                    logger.Report(LogLevel.Error, this.PreConnection.Range,
                                  Messages.Error_FretMissingForSlideOutNote);
                    return false;
                }

                if (this.PostConnection.Value == PostNoteConnection.SlideOutToLower
                    && this.Fret.Value <= context.DocumentState.GetCapoFretOffset(this.String.Value - 1))
                {
                    logger.Report(LogLevel.Warning, this.Fret.Range,
                                  Messages.Warning_FretTooLowForSlideOutNote);
                    element.PostConnection = PostNoteConnection.None;
                }
            }

            return true;
        }

        private bool ValidatePreConnection(TablatureContext context, ILogger logger, VoicePart voicePart, BeatNote element)
        {
            if (this.PreConnection == null || this.PreConnection.Value == PreNoteConnection.None)
                return true;

            if (this.PreConnection.Value == PreNoteConnection.SlideInFromHigher
                || this.PreConnection.Value == PreNoteConnection.SlideInFromLower)
            {
                if (this.Fret == null)
                {
                    logger.Report(LogLevel.Error, this.PreConnection.Range,
                                  Messages.Error_FretMissingForSlideInNote);
                    return false;
                }

                if (this.PreConnection.Value == PreNoteConnection.SlideInFromLower
                    && this.Fret.Value <= context.DocumentState.CapoFretOffsets[this.String.Value - 1])
                {
                    logger.Report(LogLevel.Warning, this.Fret.Range,
                                  Messages.Warning_FretTooLowForSlideInNote);
                    element.PreConnection = PreNoteConnection.None;
                }

                return true;
            }

            if (!this.RetrievePreConnectedNote(context, logger, element))
                return false;

            switch (this.PreConnection.Value)
            {
                case PreNoteConnection.Slide:
                    if (this.Fret == null)
                    {
                        logger.Report(LogLevel.Error, this.PreConnection.Range,
                                      Messages.Error_FretMissingForSlideNote);
                        return false;
                    }
                    if (this.Fret.Value == element.PreConnectedNote.Fret)
                    {
                        logger.Report(LogLevel.Warning, this.PreConnection.Range,
                                      Messages.Warning_SlidingToSameFret);
                        element.PreConnection = PreNoteConnection.None;
                    }
                    return true;
                case PreNoteConnection.Hammer:
                    if (this.Fret == null)
                    {
                        logger.Report(LogLevel.Error, this.PreConnection.Range,
                                      Messages.Error_FretMissingForHammerNote);
                        return false;
                    }

                    if (this.Fret.Value == element.PreConnectedNote.Fret)
                    {
                        logger.Report(LogLevel.Warning, this.Fret.Range,
                                      Messages.Warning_HammeringToSameFret);
                        element.PreConnection = PreNoteConnection.None;
                    }
                    else if (this.Fret.Value < element.PreConnectedNote.Fret)
                    {
                        logger.Report(LogLevel.Warning, this.Fret.Range,
                                      Messages.Warning_HammeringFromHigherFret);
                        element.PreConnection = PreNoteConnection.Pull;
                    }
                    return true;
                case PreNoteConnection.Pull:
                    if (this.Fret == null)
                    {
                        logger.Report(LogLevel.Error, this.PreConnection.Range,
                                      Messages.Error_FretMissingForPullNote);
                        return false;
                    }

                    if (this.Fret.Value == element.PreConnectedNote.Fret)
                    {
                        logger.Report(LogLevel.Warning, this.Fret.Range,
                                      Messages.Warning_PullingToSameFret);
                        element.PreConnection = PreNoteConnection.None;
                    }
                    else if (this.Fret.Value > element.PreConnectedNote.Fret)
                    {
                        logger.Report(LogLevel.Warning, this.Fret.Range,
                                      Messages.Warning_PullingFromLowerFret);
                        element.PreConnection = PreNoteConnection.Hammer;
                    }
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
