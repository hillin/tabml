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
        public Point Location { get; }
        public Size AvailableSize { get; }
        public PrimitiveRenderer PrimitiveRenderer => this.Owner.PrimitiveRenderer;
        public TablatureStyle Style => this.Owner.Style;

        private double[] StringCarets { get; }
        public TablatureRenderingContext TablatureRenderingContext => this.Owner;

        public RowRenderingContext(TablatureRenderingContext owner, Point location, Size availableSize)
            : base(owner)
        {
            this.Location = location;
            this.AvailableSize = availableSize;

            this.StringCarets = new double[this.Style.StringCount];
        }


        public double GetRelativePosition(double position)
        {
            return position - this.Location.X;
        }

        public void UpdateHorizontalBarLine(int stringIndex, double position)
        {
            this.DrawHorizontalBarLineTo(stringIndex, position - 10); // todo: use measure string to handle spaces

            this.StringCarets[stringIndex] = position + 10;
        }

        public void FinishHorizontalBarLines(double width)
        {
            for (var i = 0; i < this.StringCarets.Length; ++i)
            {
                if (this.StringCarets[i] < width)
                {
                    this.DrawHorizontalBarLineTo(i, width);
                    this.StringCarets[i] = width;
                }
            }
        }

        private void DrawHorizontalBarLineTo(int stringIndex, double position)
        {
            this.PrimitiveRenderer.DrawHorizontalBarLine(this.Location.X + this.StringCarets[stringIndex],
                                                         this.GetStringPosition(stringIndex),
                                                         position - this.StringCarets[stringIndex]);
        }

        public double GetBodyCeiling() => this.Location.Y + this.Style.BarTopMargin;
        public double GetBodyFloor() => this.GetBodyCeiling() + this.Style.BarLineHeight * this.Style.StringCount;

        public double GetStringPosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 0.5) * this.Style.BarLineHeight;
        public double GetStringSpacePosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + stringIndex * this.Style.BarLineHeight;

        public void DrawTie(double from, double to, int stringIndex, TiePosition tiePosition, string instruction,
                            double instructionY)
        {
            var spaceIndex = tiePosition == TiePosition.Under ? stringIndex + 1 : stringIndex;
            var y = this.GetStringSpacePosition(spaceIndex);
            this.PrimitiveRenderer.DrawTie(from + this.Location.X + 10, to + this.Location.X - 10, y, tiePosition.ToOffBarDirection());
            //todo: replace magic number
        }


        public void DrawTieInstruction(double x, double y, string instruction)
        {
            this.PrimitiveRenderer.DrawTieInstruction(x + this.Location.X, y + this.Location.Y, instruction);
        }
    }
}
