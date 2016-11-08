using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature;
using BarLine = TabML.Core.MusicTheory.BarLine;

namespace TabML.Editor.Rendering
{
    class BarRenderContext : IBarDrawingContext
    {
        public Size AvailableSize { get; }
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public Point Location { get; }
        public TablatureStyle Style { get; }

        private double[] StringPositions { get; }

        public BarRenderContext(Point location, Size availableSize, PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.Location = location;
            this.AvailableSize = availableSize;
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;

            this.StringPositions = new double[6];
        }

        public void DrawFretNumber(int stringIndex, string fretNumber, double position)
        {
            this.DrawHorizontalBarLineTo(stringIndex, position - 10); // todo: use measure string to handle spaces

            this.StringPositions[stringIndex] = position + 10;

            this.PrimitiveRenderer.DrawFretNumber(fretNumber, this.Location.X + position,
                                                  this.Location.Y + stringIndex * this.Style.BarLineHeight);
        }

        public void FinishHorizontalBarLines(double width)
        {
            for (var i = 0; i < this.StringPositions.Length; ++i)
            {
                if (this.StringPositions[i] < width)
                {
                    this.DrawHorizontalBarLineTo(i, width);
                    this.StringPositions[i] = width;
                }
            }
        }

        public void DrawBarLine(OpenBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, position);
        }

        public void DrawBarLine(CloseBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, position);
        }

        private void DrawHorizontalBarLineTo(int stringIndex, double position)
        {
            this.PrimitiveRenderer.DrawHorizontalBarLine(this.Location.X + this.StringPositions[stringIndex],
                                                         this.Location.Y + stringIndex * this.Style.BarLineHeight,
                                                         position - this.StringPositions[stringIndex]);
        }
    }
}
