using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;
using TheoreticalChord = TabML.Core.MusicTheory.Chord;

namespace TabML.Parser.Document
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
            to._time = from._time;
            to._tempo = from._tempo;
            to._tuning = from._tuning;
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
        private TimeSignature _time;

        private readonly SealableCollection<Section> _definedSections =
            new SealableCollection<Section>();

        private TempoSignature _tempo;
        private TuningSignature _tuning;
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

        public TimeSignature Time
        {
            get { return _time; }
            set
            {
                this.CheckSealed();
                _time = value;
            }
        }

        public TempoSignature Tempo
        {
            get { return _tempo; }
            set
            {
                this.CheckSealed();
                _tempo = value;
            }
        }

        public TuningSignature Tuning
        {
            get { return _tuning; }
            set
            {
                this.CheckSealed();
                _tuning = value;
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

        public bool LookupChord(string chordName, out ChordFingering fingering, out TheoreticalChord theoreticalChord)
        {
            var chord =
                this.DefinedChords.FirstOrDefault(
                    c => c.Name.Equals(chordName, StringComparison.InvariantCultureIgnoreCase));

            if (chord != null)
            {
                fingering = chord.Fingering;
                theoreticalChord = null;
                return true;
            }

            fingering = null;

            return new ChordParser().TryParse(chordName, out theoreticalChord);
        }
    }
}
