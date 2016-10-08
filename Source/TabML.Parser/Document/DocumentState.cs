using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Document
{
    class DocumentState
    {

        protected static void Clone(DocumentState from, DocumentState to)
        {
            to._currentAlternation = from._currentAlternation;
            to._alternationTextType = from._alternationTextType;
            to._alternationTextExplicity = from._alternationTextExplicity;
            to._definedAlternationIndices.AppendClone(from._definedAlternationIndices);
            to._barAppeared = from._barAppeared;
            to._capoInstructions.AppendClone(from._capoInstructions);
            to._capoFretOffsets = (int[])from._capoFretOffsets?.Clone();
            to.MinimumCapoFret = from.MinimumCapoFret;
            to._definedChords.AppendClone(from._definedChords);
            to._key = from._key;
            to._time = from._time;
            to._tempo = from._tempo;
            to._tuning = from._tuning;
            to._rhythmTemplate = from._rhythmTemplate;
            to._rhythmInstruction = from._rhythmInstruction;
            to._currentSection = from._currentSection;
            to._definedSections.AppendClone(from._definedSections);
        }

        private AlternateCommandletNode _currentAlternation;
        private AlternationTextType? _alternationTextType;
        private SectionCommandletNode _currentSection;
        private Explicity _alternationTextExplicity;
        private readonly SealableCollection<int> _definedAlternationIndices = new SealableCollection<int>();
        private bool _barAppeared;

        private readonly SealableCollection<CapoCommandletNode> _capoInstructions =
            new SealableCollection<CapoCommandletNode>();

        private int[] _capoFretOffsets;

        private readonly SealableCollection<ChordCommandletNode> _definedChords =
            new SealableCollection<ChordCommandletNode>();

        private KeyCommandletNode _key;
        private TimeSignatureCommandletNode _time;

        private readonly SealableCollection<SectionCommandletNode> _definedSections =
            new SealableCollection<SectionCommandletNode>();

        private TempoCommandletNode _tempo;
        private TuningCommandletNode _tuning;
        private RhythmCommandletNode _rhythmInstruction;
        private Rhythm _rhythmTemplate;


        public AlternateCommandletNode CurrentAlternation
        {
            get { return _currentAlternation; }
            set
            {
                this.CheckSealed();
                _currentAlternation = value;
            }
        }

        public AlternationTextType? AlternationTextType
        {
            get { return _alternationTextType; }
            set
            {
                this.CheckSealed();
                _alternationTextType = value;
            }
        }


        public Explicity AlternationTextExplicity
        {
            get { return _alternationTextExplicity; }
            set
            {
                this.CheckSealed();
                _alternationTextExplicity = value;
            }
        }

        public ICollection<int> DefinedAlternationIndices => _definedAlternationIndices;

        public bool BarAppeared
        {
            get { return _barAppeared; }
            set
            {
                this.CheckSealed();
                _barAppeared = value;
            }
        }

        public ICollection<CapoCommandletNode> CapoInstructions => _capoInstructions;

        public int[] CapoFretOffsets
        {
            get { return _capoFretOffsets; }
            set
            {
                this.CheckSealed();
                _capoFretOffsets = value;
                this.MinimumCapoFret = _capoFretOffsets.Min();
            }
        }

        public int MinimumCapoFret { get; private set; }

        public ICollection<ChordCommandletNode> DefinedChords => _definedChords;

        public KeyCommandletNode Key
        {
            get { return _key; }
            set
            {
                this.CheckSealed();
                _key = value;
            }
        }

        public TimeSignatureCommandletNode Time
        {
            get { return _time; }
            set
            {
                this.CheckSealed();
                _time = value;
            }
        }

        public TempoCommandletNode Tempo
        {
            get { return _tempo; }
            set
            {
                this.CheckSealed();
                _tempo = value;
            }
        }

        public TuningCommandletNode Tuning
        {
            get { return _tuning; }
            set
            {
                this.CheckSealed();
                _tuning = value;
            }
        }

        public Rhythm RhythmTemplate
        {
            get { return _rhythmTemplate; }
            set
            {
                this.CheckSealed();
                _rhythmTemplate = value;
            }
        }

        public RhythmCommandletNode RhythmInstruction
        {
            get { return _rhythmInstruction; }
            set
            {
                this.CheckSealed();
                _rhythmInstruction = value;
            }
        }

        public SectionCommandletNode CurrentSection
        {
            get { return _currentSection; }
            set
            {
                this.CheckSealed();
                _currentSection = value;
            }
        }

        public ICollection<SectionCommandletNode> DefinedSections => _definedSections;

        public bool IsSealed { get; private set; }


        protected void Seal()
        {
            this.IsSealed = true;
            _definedAlternationIndices.Seal();
            _capoInstructions.Seal();
            _definedChords.Seal();
            _definedSections.Seal();
        }

        private void CheckSealed()
        {
            if (this.IsSealed)
                throw new InvalidOperationException("this DocumentState is sealed and uneditable");
        }

        public bool LookupChord(string chordName, out int[] fingeringIndices)
        {
            var chord =
                this.DefinedChords.FirstOrDefault(
                    c => c.Name.Value.Equals(chordName, StringComparison.InvariantCultureIgnoreCase));

            if (chord != null)
            {
                fingeringIndices = chord.Fingering.GetFingeringIndices();
                return true;
            }

            // todo: look up in chord library

            fingeringIndices = null;
            return false;
        }
    }
}
