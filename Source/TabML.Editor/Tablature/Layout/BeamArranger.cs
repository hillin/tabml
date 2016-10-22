using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature.Layout
{
    class BeamArranger
    {
        private readonly BaseNoteValue _beamNoteValue;
        private readonly Stack<ArrangedBeam> _beamStack;
        private ArrangedBeam _currentBeam;
        private ArrangedBeam _currentRootBeam;
        private readonly List<ArrangedBeam> _rootBeams;

        private PreciseDuration _currentCapacity;
        private PreciseDuration _duration;

        public BeamArranger(BaseNoteValue beamNoteValue)
        {
            _beamNoteValue = beamNoteValue;
            _beamStack = new Stack<ArrangedBeam>();
            _rootBeams = new List<ArrangedBeam>();
            _currentCapacity = PreciseDuration.Zero;
            _duration = PreciseDuration.Zero;
        }

        public IEnumerable<ArrangedBeam> GetRootBeams()
        {
            return _rootBeams;
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
                    _beamStack.Pop();
                    _currentBeam = _beamStack.Peek();
                    continue;
                }

                // this is the root beam
                _currentBeam.Elements.Add(beat);
                _duration += beat.GetDuration();
                return;
            }

            while (beatNoteValue < _currentBeam.BeatNoteValue)
            {
                var newBeam = new ArrangedBeam(_currentBeam.BeatNoteValue.Half());
                _beamStack.Push(newBeam);
                _currentBeam.Elements.Add(newBeam);
                _currentBeam = newBeam;
            }

            Debug.Assert(beatNoteValue == _currentBeam.BeatNoteValue);
            _currentBeam.Elements.Add(beat);
            _duration += beat.GetDuration();
        }

        private void StartRootBeam()
        {
            _currentBeam = new ArrangedBeam(_beamNoteValue.Half());
            _currentRootBeam = _currentBeam;
            _rootBeams.Add(_currentBeam);

            _beamStack.Clear();
            _beamStack.Push(_currentBeam);

            while (_currentCapacity <= _duration)
                _currentCapacity += _beamNoteValue.GetDuration();
        }
    }
}
