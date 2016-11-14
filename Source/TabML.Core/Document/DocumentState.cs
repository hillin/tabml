using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TheoreticalChord = TabML.Core.MusicTheory.Chord;

namespace TabML.Core.Document
{
    public class DocumentState
    {

        protected static void Clone(DocumentState from, DocumentState to)
        {
            to._currentAlternation = from._currentAlternation;
            to._alternationTextType = from._alternationTextType;
            to._alternationTextExplicity = from._alternationTextExplicity;
            to._definedAlternationIndices.AppendClone(from._definedAlternationIndices);
            to._barAppeared = from._barAppeared;
            to._capos.AppendClone(from._capos);
            to._capoFretOffsets = (int[])from._capoFretOffsets?.Clone();
            to.MinimumCapoFret = from.MinimumCapoFret;
            to._definedChords.AppendClone(from._definedChords);
            to._keySignature = from._keySignature;
            to._timeSignature = from._timeSignature;
            to._tempoSignature = from._tempoSignature;
            to._tuningSignature = from._tuningSignature;
            to._rhythmTemplate = from._rhythmTemplate;
            to._currentSection = from._currentSection;
            to._definedSections.AppendClone(from._definedSections);
        }

        private Alternation _currentAlternation;
        private AlternationTextType? _alternationTextType;
        private Section _currentSection;
        private Explicity _alternationTextExplicity;
        private readonly SealableCollection<int> _definedAlternationIndices = new SealableCollection<int>();
        private bool _barAppeared;

        private readonly SealableCollection<Capo> _capos =
            new SealableCollection<Capo>();

        private int[] _capoFretOffsets;

        private readonly SealableCollection<ChordDefinition> _definedChords =
            new SealableCollection<ChordDefinition>();

        private KeySignature _keySignature;
        private TimeSignature _timeSignature;

        private readonly SealableCollection<Section> _definedSections =
            new SealableCollection<Section>();

        private TempoSignature _tempoSignature;
        private TuningSignature _tuningSignature;
        private RhythmTemplate _rhythmTemplate;


        public Alternation CurrentAlternation
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

        public ICollection<Capo> Capos => _capos;

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

        public ICollection<ChordDefinition> DefinedChords => _definedChords;

        public KeySignature KeySignature
        {
            get { return _keySignature; }
            set
            {
                this.CheckSealed();
                _keySignature = value;
            }
        }

        public TimeSignature TimeSignature
        {
            get { return _timeSignature; }
            set
            {
                this.CheckSealed();
                _timeSignature = value;
            }
        }

        public Time Time => _timeSignature?.Time ?? Defaults.Time;

        public TempoSignature TempoSignature
        {
            get { return _tempoSignature; }
            set
            {
                this.CheckSealed();
                _tempoSignature = value;
            }
        }



        public TuningSignature TuningSignature
        {
            get { return _tuningSignature; }
            set
            {
                this.CheckSealed();
                _tuningSignature = value;
            }
        }

        public RhythmTemplate RhythmTemplate
        {
            get { return _rhythmTemplate; }
            set
            {
                this.CheckSealed();
                _rhythmTemplate = value;
            }
        }

        public Section CurrentSection
        {
            get { return _currentSection; }
            set
            {
                this.CheckSealed();
                _currentSection = value;
            }
        }

        public ICollection<Section> DefinedSections => _definedSections;
        public bool IsSealed { get; private set; }


        protected void Seal()
        {
            this.IsSealed = true;
            _definedAlternationIndices.Seal();
            _capos.Seal();
            _definedChords.Seal();
            _definedSections.Seal();
        }

        private void CheckSealed()
        {
            if (this.IsSealed)
                throw new InvalidOperationException("this DocumentState is sealed and uneditable");
        }


    }
}
