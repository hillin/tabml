using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    class BeatRenderingContext : RenderingContextBase<BarRenderingContext>
    {
        private readonly List<int> _artificialHarmonicFrets;
        public IReadOnlyList<int> ArtificialHarmonicFrets => _artificialHarmonicFrets;

        public Dictionary<double, string> ConnectionInstructions { get; }

        public BeatRenderingContext(BarRenderingContext owner) : base(owner)
        {
            _artificialHarmonicFrets = new List<int>();
            this.ConnectionInstructions = new Dictionary<double, string>();
        }

        public void AddArtificialHarmonic(int fret)
        {
            _artificialHarmonicFrets.Add(fret);
        }

        public void AddConnectionInstruction(double position, string instruction)
        {
            this.ConnectionInstructions.Add(position, instruction);
        }
    }
}
