using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
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

        private double[] StringCarets { get; }

        public BarRenderContext(Point location, Size availableSize, PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.Location = location;
            this.AvailableSize = availableSize;
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;

            this.StringCarets = new double[6];
        }

        public void DrawFretNumber(int stringIndex, string fretNumber, double position)
        {
            this.DrawHorizontalBarLineTo(stringIndex, position - 10); // todo: use measure string to handle spaces

            this.StringCarets[stringIndex] = position + 10;

            this.PrimitiveRenderer.DrawFretNumber(fretNumber, this.Location.X + position,
                                                  this.GetStringPosition(stringIndex));
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

        public void DrawBarLine(OpenBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, this.Location.X + position, this.GetStringPosition(0));
        }

        public void DrawBarLine(CloseBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, this.Location.X + position, this.GetStringPosition(0));
        }

        private double OffsetByVoicePart(double baseValue, double offset, VoicePart voicePart) => voicePart == VoicePart.Treble ? baseValue - offset : baseValue + offset;

        public void DrawStem(double position, VoicePart voicePart)
        {
            var yBase = voicePart == VoicePart.Treble ? this.GetBodyCeiling() : this.GetBodyFloor();
            var yFrom = this.OffsetByVoicePart(yBase, this.Style.NoteStemOffset, voicePart);
            var yTo = this.OffsetByVoicePart(yBase, this.Style.NoteTailOffset, voicePart);

            this.PrimitiveRenderer.DrawStem(this.Location.X + position, Math.Min(yFrom, yTo), Math.Max(yFrom, yTo));
        }

        private double GetBodyCeiling() => this.Location.Y + this.Style.BarTopMargin;
        private double GetBodyFloor() => this.Location.Y + this.Style.BarLineHeight * this.Style.StringCount;

        private double GetStringPosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 0.5) * this.Style.BarLineHeight;

        private void DrawHorizontalBarLineTo(int stringIndex, double position)
        {
            this.PrimitiveRenderer.DrawHorizontalBarLine(this.Location.X + this.StringCarets[stringIndex],
                                                         this.GetStringPosition(stringIndex),
                                                         position - this.StringCarets[stringIndex]);
        }

        public void DrawFlag(NoteValue noteValue, double position, VoicePart voicePart)
        {
            throw new NotImplementedException();
        }

        public void DrawHalfBeam(BaseNoteValue noteValue, double position, VoicePart voicePart, bool isLastOfBeam)
        {
            throw new NotImplementedException();
        }

        public void DrawNoteValueAugment(NoteValueAugment noteValueAugment, double position, VoicePart voicePart)
        {
            throw new NotImplementedException();
        }

        public void DrawBeam(BaseNoteValue beatNoteValue, double @from, double to)
        {
            throw new NotImplementedException();
        }
    }
}
