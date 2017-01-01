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
                {VoicePart.Bass, this.CreatehHeightMap(availableSize)},
                {VoicePart.Treble, this.CreatehHeightMap(availableSize)},
            };
        }

        private HeightMap CreatehHeightMap(Size availableSize)
        {
            return new HeightMap((int)Math.Ceiling(availableSize.Width), HeightMapSampleRate, this.Style.MinimumNoteTailOffset + this.Style.NoteTailVerticalMargin);
        }

        public HeightMap GetHeightMap(VoicePart voicePart)
        {
            return _heightMaps[voicePart];
        }

        public double GetRelativeX(double position)
        {
            return position - this.Location.X;
        }

        public double GetRelativeY(double position)
        {
            return position - this.Location.Y;
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

        /// <summary>
        /// Gets the absolute location of the top-most bar line
        /// </summary>
        public double GetBodyCeiling() => this.Location.Y + this.Style.BarTopMargin;

        /// <summary>
        /// Gets the absolute location of the bottom-most bar line
        /// </summary>
        public double GetBodyFloor() => this.GetBodyCeiling() + this.Style.BarLineHeight * this.Style.StringCount;

        public double GetStringPosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 0.5) * this.Style.BarLineHeight;
        public double GetStringSpacePosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + stringIndex * this.Style.BarLineHeight;

        // from and to are absolute positions
        public Task<Rect> DrawTie(double from, double to, int stringIndex, TiePosition tiePosition, string instruction,
                            double instructionY)
        {
            var spaceIndex = tiePosition == TiePosition.Under ? stringIndex + 1 : stringIndex;
            var y = this.GetStringSpacePosition(spaceIndex);
            return this.PrimitiveRenderer.DrawTie(from, to, y, tiePosition.ToOffBarDirection());
        }

        /// <summary>
        /// Ensure height spanned between <paramref name="x0" /> and <paramref name="x1" /> in the 
        /// height map by selecting between <paramref name="y0" /> and <paramref name="y1" /> according to 
        /// the specified <paramref name="voicePart"/>
        /// </summary>
        /// <remarks>All coordinates are absolute</remarks>
        public void EnsureHeight(VoicePart voicePart, double x0, double x1, double y0, double y1, double vMargin = 0)
        {
            double height;
            switch (voicePart)
            {
                case VoicePart.Treble:
                    height = this.GetBodyCeiling() - Math.Min(y0, y1);
                    break;
                case VoicePart.Bass:
                    height = Math.Max(y0, y1) - this.GetBodyFloor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voicePart), voicePart, null);
            }

            _heightMaps[voicePart].EnsureHeight(this.GetRelativeX(x0), x1 - x0, height + vMargin);
        }

        public void EnsureHeight(VoicePart voicePart, Rect bounds)
        {
            this.EnsureHeight(voicePart, bounds.Left, bounds.Right, bounds.Top, bounds.Bottom);
        }

        /// <summary>
        /// Ensure height spanned between <paramref name="x0" /> and <paramref name="x1" /> in the 
        /// height map by lerping between <paramref name="y0" /> and <paramref name="y1" />.
        /// </summary>
        /// <remarks>All coordinates are absolute</remarks>
        public void EnsureHeightSloped(VoicePart voicePart, double x0, double x1, double y0, double y1, double vMargin, double hMargin)
        {
            switch (voicePart)
            {
                case VoicePart.Treble:
                    y0 = this.GetBodyCeiling() - y0;
                    y1 = this.GetBodyCeiling() - y1;
                    break;
                case VoicePart.Bass:
                    y0 = y0 - this.GetBodyFloor();
                    y1 = y1 - this.GetBodyFloor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voicePart), voicePart, null);
            }

            _heightMaps[voicePart].EnsureHeight(this.GetRelativeX(x0), x1 - x0, y0 + vMargin, y1 + vMargin, hMargin);
        }

        public double GetHeight(VoicePart voicePart, double x)
        {
            switch (voicePart)
            {
                case VoicePart.Treble:
                    return this.GetBodyCeiling() - _heightMaps[voicePart].GetHeight(this.GetRelativeX(x));
                case VoicePart.Bass:
                    return this.GetBodyFloor() + _heightMaps[voicePart].GetHeight(this.GetRelativeX(x));
                default:
                    throw new ArgumentOutOfRangeException(nameof(voicePart), voicePart, null);
            }
        }

        public void DebugDrawHeightMaps()
        {
            var ceiling = this.GetBodyCeiling();
            this.DebugDrawHeightMap(_heightMaps[VoicePart.Treble], h => ceiling - h);

            var floor = this.GetBodyFloor();
            this.DebugDrawHeightMap(_heightMaps[VoicePart.Bass], h => floor + h);
        }

        private void DebugDrawHeightMap(HeightMap heightMap, Func<double, double> heightConverter)
        {
            this.PrimitiveRenderer.DebugDrawHeightMap(heightMap.DebugGetVertices().Select(p => new Point(p.X + this.Location.X, heightConverter(p.Y))));
        }
    }
}
