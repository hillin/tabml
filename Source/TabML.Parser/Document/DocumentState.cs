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
            to._currentAlternationIndices = (int[])from._currentAlternationIndices.Clone();
            to._alternationTextType = from._alternationTextType;
            to._currentSectionName = from._currentSectionName;
            to._alternationTextExplicity = from._alternationTextExplicity;
            to._definedAlternationIndices.CloneAndAppend(from._definedAlternationIndices);
            to._barAppeared = from._barAppeared;
            to._capoInstructions.CloneAndAppend(from._capoInstructions);
            to._capoFretOffsets = (int[])from._capoFretOffsets.Clone();
            to._definedChords.CloneAndAppend(from._definedChords);
            to._key = from._key;
            to._time = from._time;
        }

        private int[] _currentAlternationIndices = new int[0];
        private AlternationTextType? _alternationTextType;
        private string _currentSectionName;
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


        public int[] CurrentAlternationIndices
        {
            get { return _currentAlternationIndices; }
            set
            {
                this.CheckSealed();
                _currentAlternationIndices = value;
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

        public string CurrentSectionName
        {
            get { return _currentSectionName; }
            set
            {
                this.CheckSealed();
                _currentSectionName = value;
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
            }
        }

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

        public bool IsSealed { get; private set; }


        protected void Seal()
        {
            this.IsSealed = true;
            _definedAlternationIndices.Seal();
            _capoInstructions.Seal();
            _definedChords.Seal();
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
