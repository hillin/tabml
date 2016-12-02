using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabML.Editor.Rendering
{
    class RowDrawingContext
    {
        public Point Location { get; }
        public Size AvailableSize { get; }
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }

        private double[] StringCarets { get; }

        public RowDrawingContext(Point location, Size availableSize, PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.Location = location;
            this.AvailableSize = availableSize;
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;

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
    }
}
