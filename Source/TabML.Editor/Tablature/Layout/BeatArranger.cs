using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature.Layout
{
    class BeatArranger
    {
        public VoicePart VoicePart { get; }
        private readonly BaseNoteValue _beamNoteValue;
        private readonly Stack<ArrangedBeam> _beamStack;
        private ArrangedBeam _currentBeam;
        private ArrangedBeam _currentRootBeam;
        private readonly List<IBeatElement> _rootBeats;

        private PreciseDuration _currentCapacity;
        private PreciseDuration _duration;

        public BeatArranger(BaseNoteValue beamNoteValue, VoicePart voicePart)
        {
            this.VoicePart = voicePart;
            _beamNoteValue = beamNoteValue;
            _beamStack = new Stack<ArrangedBeam>();
            _rootBeats = new List<IBeatElement>();
            _currentCapacity = PreciseDuration.Zero;
            _duration = PreciseDuration.Zero;
        }

        public IEnumerable<IBeatElement> GetRootBeats()
        {
            return _rootBeats;
        }

        public void Finish()
        {
            this.FinishBeam();
        }

        private void FinishBeam()
        {
            var popedBeam = _beamStack.Pop();
            _currentBeam = _beamStack.Count > 0 ? _beamStack.Peek() : null;

            Debug.Assert(popedBeam.Elements.Count > 0);

            // check for reduceable beam
            if (popedBeam.Elements.Count > 1)
                return;

            var beat = popedBeam.Elements[0] as ArrangedBarBeat;
            if (beat == null)
                return;

            // this beam contains only one beat, reduce it to a beat
            beat.OwnerBeam = null;
            if (_currentBeam == null) // root
                _rootBeats[_rootBeats.Count - 1] = beat;
            else
                _currentBeam.Elements[_currentBeam.Elements.Count - 1] = beat;
        }

        public void AddBeat(ArrangedBarBeat beat)
        {
            if (_currentBeam == null)
                this.StartRootBeam();
            else if (_duration >= _currentCapacity)
                this.StartRootBeam();
            else if (!_currentBeam.MatchesTuplet(beat))
            {
                this.StartRootBeam();
                _currentBeam.Tuplet = beat.Beat.NoteValue.Tuplet;
            }

            var beatNoteValue = beat.Beat.NoteValue.Base;

            Debug.Assert(_currentBeam != null, "_currentBeam != null");
            while (beatNoteValue > _currentBeam.BeatNoteValue)
            {
                if (_beamStack.Count > 1)
                {
                    this.FinishBeam();
                    continue;
                }

                // this is the root beam
                this.AddToCurrentBeam(beat);
                return;
            }

            while (beatNoteValue < _currentBeam.BeatNoteValue)
            {
                var newBeam = new ArrangedBeam(_currentBeam.BeatNoteValue.Half(), _currentBeam.VoicePart, false);
                _beamStack.Push(newBeam);
                _currentBeam.Elements.Add(newBeam);
                _currentBeam = newBeam;
            }

            Debug.Assert(beatNoteValue == _currentBeam.BeatNoteValue);
            this.AddToCurrentBeam(beat);
        }

        private void AddToCurrentBeam(ArrangedBarBeat beat)
        {
            _currentBeam.Elements.Add(beat);
            beat.OwnerBeam = _currentBeam;
            _duration += beat.GetDuration();
        }

        private void StartRootBeam()
        {
            while (_beamStack.Count > 0)
            {
                this.FinishBeam();
            }

            _currentBeam = new ArrangedBeam(_beamNoteValue.Half(), this.VoicePart, true);

            _currentRootBeam = _currentBeam;
            _rootBeats.Add(_currentBeam);

            _beamStack.Clear();
            _beamStack.Push(_currentBeam);

            while (_currentCapacity <= _duration)
                _currentCapacity += _beamNoteValue.GetDuration();
        }
    }
}
