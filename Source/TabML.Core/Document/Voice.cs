using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Voice : VirtualElement, IBeatElementContainer
    {
        public List<IBeatElement> BeatElements { get; }
        List<IBeatElement> IBeatElementContainer.Elements => this.BeatElements;

        public Bar OwnerBar { get; }
        public VoicePart VoicePart { get; }


        private bool _isTerminatedWithRest;
        public bool IsTerminatedWithRest
        {
            get { return _isTerminatedWithRest; }
            set
            {
                _isTerminatedWithRest = value;
                if (_isTerminatedWithRest)
                    for (var i = 0; i < this.LastNoteOnStrings.Length; ++i)
                        this.LastNoteOnStrings[i] = null;
            }
        }

        /// <summary>
        /// the last notes appeared on each string of this voice. this propery is graduately updated to reflect
        /// the state of the voice, in order to determine the predecessor note to be tied to another note
        /// </summary>
        public BeatNote[] LastNoteOnStrings { get; } = new BeatNote[Defaults.Strings];

        public Voice(Bar ownerBar, VoicePart voicePart)
        {
            this.OwnerBar = ownerBar;
            this.VoicePart = voicePart;
            this.BeatElements = new List<IBeatElement>();
        }
        public PreciseDuration GetDuration() => this.BeatElements.Sum(n => n.GetDuration());
        
    }
}
