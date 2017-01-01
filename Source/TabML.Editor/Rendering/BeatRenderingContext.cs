using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    class BeatRenderingContext : RenderingContextBase<BarRenderingContext>
    {
        public struct ArtificialHarmonicFretInfo
        {
            // ReSharper disable once InconsistentNaming
            public bool IsOn12thFret => this.ArtificialHarmonicFret - this.BaseFret == 12;
            public int StringIndex { get; }
            public int BaseFret { get; }
            public int ArtificialHarmonicFret { get; }

            public ArtificialHarmonicFretInfo(int stringIndex, int baseFret, int artificialHarmonicFret)
            {
                this.StringIndex = stringIndex;
                this.BaseFret = baseFret;
                this.ArtificialHarmonicFret = artificialHarmonicFret;
            }

        }

        public struct ConnectionInstructionInfo
        {
            public int StringIndex { get; }
            public double Position { get; }
            public string Instruction { get; }

            public ConnectionInstructionInfo(int stringIndex, double position, string instruction)
            {
                this.StringIndex = stringIndex;
                this.Position = position;
                this.Instruction = instruction;
            }
        }

        public class ConnectionInstructionPositionEqualityComparer : IEqualityComparer<double>
        {
            private readonly double _tolerance;

            public ConnectionInstructionPositionEqualityComparer(double tolerance)
            {
                _tolerance = tolerance;
            }

            public bool Equals(double x, double y)
            {
                return Math.Abs(x - y) <= _tolerance;
            }

            public int GetHashCode(double obj)
            {
                return (Math.Round(obj / _tolerance) * _tolerance).GetHashCode();
            }
        }

        private readonly List<ArtificialHarmonicFretInfo> _artificialHarmonicFrets;
        public IReadOnlyList<ArtificialHarmonicFretInfo> ArtificialHarmonicFrets => _artificialHarmonicFrets;

        public List<ConnectionInstructionInfo> ConnectionInstructions { get; }

        public BeatRenderingContext(BarRenderingContext owner) : base(owner)
        {
            _artificialHarmonicFrets = new List<ArtificialHarmonicFretInfo>();
            this.ConnectionInstructions = new List<ConnectionInstructionInfo>();
        }

        public void AddArtificialHarmonic(int stringIndex, int baseFret, int artificialHarmonicFret)
        {
            _artificialHarmonicFrets.Add(new ArtificialHarmonicFretInfo(stringIndex, baseFret, artificialHarmonicFret));
        }

        private void AddConnectionInstruction(int stringIndex, double position, string instruction)
        {
            this.ConnectionInstructions.Add(new ConnectionInstructionInfo(stringIndex, position, instruction));
        }

        public async Task DrawConnection(IRootElementRenderer rootRenderer, NoteConnection connection, Beat from,
                                         Beat to, int stringIndex, TiePosition tiePosition)
        {
            var bounds = await
                NoteConnectionRenderer.DrawConnection(rootRenderer, connection, from, to, stringIndex, tiePosition);

            var instructionPosition = (bounds.Left + bounds.Right) / 2;

            switch (connection)
            {
                case NoteConnection.Slide:
                    this.AddConnectionInstruction(stringIndex, instructionPosition, "sl.");
                    break;
                case NoteConnection.SlideInFromHigher:
                case NoteConnection.SlideInFromLower:
                case NoteConnection.SlideOutToHigher:
                case NoteConnection.SlideOutToLower:
                    this.AddConnectionInstruction(stringIndex, instructionPosition, "gl.");
                    break;
                case NoteConnection.Hammer:
                    this.AddConnectionInstruction(stringIndex, instructionPosition, "h.");
                    break;
                case NoteConnection.Pull:
                    this.AddConnectionInstruction(stringIndex, instructionPosition, "p.");
                    break;
            }
        }
    }
}
