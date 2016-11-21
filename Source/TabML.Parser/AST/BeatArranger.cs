using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class BeatArranger
    {
        public VoicePart VoicePart { get; }
        private readonly BaseNoteValue _beamNoteValue;
        private readonly Stack<Beam> _beamStack;
        private Beam _currentBeam;
        private Beam _currentRootBeam;
        private readonly List<IBeatElement> _rootBeats;

        private PreciseDuration _currentCapacity;
        private PreciseDuration _duration;

        public BeatArranger(BaseNoteValue beamNoteValue, VoicePart voicePart)
        {
            this.VoicePart = voicePart;
            _beamNoteValue = beamNoteValue;
            _beamStack = new Stack<Beam>();
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
            this.FinishBeamStack();
        }

        private void FinishBeam()
        {
            var popedBeam = _beamStack.Pop();
            _currentBeam = _beamStack.Count > 0 ? _beamStack.Peek() : null;

            if (popedBeam.Elements.Count == 0)
            {
                _rootBeats.Remove(popedBeam);
                return;
            }
            var rearrangedElements = BeatArranger.RearrangeBeam(popedBeam);

            // the rearrangement didn't change the beam
            if (rearrangedElements.Count == 1 && rearrangedElements.First() == popedBeam)
                return;

            if (_currentBeam == null) // root
            {
                _rootBeats.RemoveAt(_rootBeats.Count - 1);
                _rootBeats.AddRange(rearrangedElements);
                foreach (var beat in rearrangedElements.OfType<Beat>())
                    beat.OwnerBeam = null;
            }
            else
            {
                _currentBeam.Elements.RemoveAt(_currentBeam.Elements.Count - 1);
                _currentBeam.Elements.AddRange(rearrangedElements);
                foreach (var beat in rearrangedElements.OfType<Beat>())
                    beat.OwnerBeam = _currentBeam;
            }
        }

        private static ICollection<IBeatElement> RearrangeBeam(Beam beam)
        {
            var headRests = new List<IBeatElement>();
            var indexOfFirstNonRestBeat = 0;
            for (var i = indexOfFirstNonRestBeat; i < beam.Elements.Count; ++i)
            {
                var element = beam.Elements[i];
                var beat = element as Beat;
                if (beat == null)
                    break;

                if (!beat.IsRest)
                    break;

                headRests.Add(beat);
                indexOfFirstNonRestBeat = i + 1;
            }

            var tailRests = new List<IBeatElement>();
            var indexOfLastNonRestBeat = beam.Elements.Count - 1;
            for (var i = indexOfLastNonRestBeat; i > indexOfFirstNonRestBeat; --i)
            {
                var element = beam.Elements[i];
                var beat = element as Beat;
                if (beat == null)
                    break;

                if (!beat.IsRest)
                    break;

                tailRests.Add(beat);
                indexOfLastNonRestBeat = i - 1;
            }

            beam.Elements.RemoveRange(indexOfLastNonRestBeat + 1, beam.Elements.Count - indexOfLastNonRestBeat - 1);
            beam.Elements.RemoveRange(0, indexOfFirstNonRestBeat);

            var result = new List<IBeatElement>(headRests);

            if (beam.Elements.Count == 1 && beam.Elements[0] is Beat)
                // this beam contains only one beat now, reduce it to a beat
                result.Add(beam.Elements[0]);
            else if (beam.Elements.Count > 0)
                result.Add(beam);

            result.AddRange(tailRests);

            return result;
        }

        public void AddBeat(Beat beat)
        {

            if (beat.NoteValue.Base >= _beamNoteValue) // beat too long to be beamed
            {
                this.InsertUnbeamedBeat(beat);
                return;
            }

            var tuplet = beat.NoteValue.Tuplet;

            if (_currentBeam != null && _currentBeam.GetIsTupletFull()) // tuplet full
                this.FinishBeam();

            if (_currentBeam == null) // initialize root beam
                this.StartRootBeam(tuplet);
            else if (_duration >= _currentCapacity) // beam full
                this.FinishAndStartRootBeam(tuplet);
            else if (!_currentBeam.MatchesTuplet(beat)) // tuplet mismatch
                this.FinishAndStartRootBeam(tuplet);


            var beatNoteValue = beat.NoteValue.Base;

            Debug.Assert(_currentBeam != null, "_currentBeam != null");
            while (beatNoteValue > _currentBeam.BeatNoteValue)
            {
                Debug.Assert(_beamStack.Count > 0);
                this.FinishBeam();
            }

            // create sub-beams if beat is too short
            while (beatNoteValue < _currentBeam.BeatNoteValue)
            {
                this.StartChildBeam(tuplet);
            }

            Debug.Assert(beatNoteValue == _currentBeam.BeatNoteValue);
            this.AddToCurrentBeam(beat);
        }

        private void FinishAndStartChildBeam(int? tuplet)
        {
            this.FinishBeam();
            this.StartChildBeam(tuplet);
        }

        private void InsertUnbeamedBeat(Beat beat)
        {
            this.FinishBeamStack();
            _rootBeats.Add(beat);
            _duration += beat.GetDuration();
        }

        private void AddToCurrentBeam(Beat beat)
        {
            _currentBeam.Elements.Add(beat);
            beat.OwnerBeam = _currentBeam;
            _duration += beat.GetDuration();
        }

        private void FinishAndStartRootBeam(int? tuplet)
        {
            this.FinishBeamStack();

            this.StartRootBeam(tuplet);
        }

        private void StartChildBeam(int? tuplet)
        {
            var newBeam = new Beam(_currentBeam.BeatNoteValue.Half(), _currentBeam.VoicePart, false)
            {
                Tuplet = tuplet
            };
            _beamStack.Push(newBeam);
            _currentBeam.Elements.Add(newBeam);
            _currentBeam = newBeam;
        }

        private void StartRootBeam(int? tuplet)
        {
            _currentBeam = new Beam(_beamNoteValue.Half(), this.VoicePart, true) { Tuplet = tuplet };

            _currentRootBeam = _currentBeam;
            _rootBeats.Add(_currentBeam);

            _beamStack.Push(_currentBeam);

            while (_currentCapacity <= _duration)
                _currentCapacity += _beamNoteValue.GetDuration();
        }

        private void FinishBeamStack()
        {
            while (_beamStack.Count > 0)
            {
                this.FinishBeam();
            }
        }
    }
}
