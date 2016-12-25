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
    class RowRenderingContext : RenderingContextBase<TablatureRenderingContext>
    {
        private const int HeightMapSampleRate = 1;

        private readonly double[] _stringCarets;

        private readonly Dictionary<VoicePart, HeightMap> _heightMaps;
        public Point Location { get; }
        public Size AvailableSize { get; }

        public Point BottomRight => this.Location + new Vector(this.AvailableSize.Width, this.AvailableSize.Height);
        public PrimitiveRenderer PrimitiveRenderer => this.Owner.PrimitiveRenderer;
        public TablatureStyle Style => this.Owner.Style;
        public TablatureRenderingContext TablatureRenderingContext => this.Owner;

        public RowRenderingContext(TablatureRenderingContext owner, Point location, Size availableSize)
            : base(owner)
        {
            this.Location = location;
            this.AvailableSize = availableSize;

            _stringCarets = new double[this.Style.StringCount];
            _heightMaps = new Dictionary<VoicePart, HeightMap>
            {
                {VoicePart.Bass, new HeightMap((int) Math.Ceiling(availableSize.Width), HeightMapSampleRate)},
                {VoicePart.Treble, new HeightMap((int) Math.Ceiling(availableSize.Width), HeightMapSampleRate)},
            };

            _heightMaps[VoicePart.Bass].Fill(this.GetBodyFloor() + this.Style.MinimumNoteTailOffset);
            _heightMaps[VoicePart.Treble].Fill(this.GetBodyCeiling() - this.Style.MinimumNoteTailOffset);

        }

        public HeightMap GetHeightMap(VoicePart voicePart)
        {
            return _heightMaps[voicePart];
        }

        public double GetRelativePosition(double position)
        {
            return position - this.Location.X;
        }

        public void UpdateHorizontalBarLine(int stringIndex, double left, double right)
        {
            this.DrawHorizontalBarLineTo(stringIndex, left - this.Style.NoteMargin);
            _stringCarets[stringIndex] = right + this.Style.NoteMargin;
        }

        public void FinishHorizontalBarLines(double width)
        {
            for (var i = 0; i < _stringCarets.Length; ++i)
            {
                if (!(_stringCarets[i] < width))
                    continue;

                this.DrawHorizontalBarLineTo(i, width);
                _stringCarets[i] = width;
            }
        }

        private void DrawHorizontalBarLineTo(int stringIndex, double position)
        {
            this.PrimitiveRenderer.DrawHorizontalBarLine(this.Location.X + _stringCarets[stringIndex],
                                                         this.GetStringPosition(stringIndex),
                                                         position - _stringCarets[stringIndex]);
        }

        public double GetBodyCeiling() => this.Location.Y + this.Style.BarTopMargin;
        public double GetBodyFloor() => this.GetBodyCeiling() + this.Style.BarLineHeight * this.Style.StringCount;

        public double GetStringPosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 0.5) * this.Style.BarLineHeight;
        public double GetStringSpacePosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + stringIndex * this.Style.BarLineHeight;

        // from and to are absolute positions
        public void DrawTie(double from, double to, int stringIndex, TiePosition tiePosition, string instruction,
                            double instructionY)
        {
            var spaceIndex = tiePosition == TiePosition.Under ? stringIndex + 1 : stringIndex;
            var y = this.GetStringSpacePosition(spaceIndex);
            this.PrimitiveRenderer.DrawTie(from, to, y, tiePosition.ToOffBarDirection());
        }


        public void DrawTieInstruction(double x, double y, string instruction)
        {
            this.PrimitiveRenderer.DrawTieInstruction(x + this.Location.X, y + this.Location.Y, instruction);
        }


    }
}
