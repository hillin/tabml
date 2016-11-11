using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core;
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
            this.Style = style;

            this.StringCarets = new double[style.StringCount];
        }

        public void DrawFretNumber(int stringIndex, string fretNumber, double position, double horizontalOffset, bool isHalfOrLonger)
        {
            this.DrawHorizontalBarLineTo(stringIndex, position - 10); // todo: use measure string to handle spaces

            this.StringCarets[stringIndex] = position + 10;

            this.PrimitiveRenderer.DrawFretNumber(fretNumber, this.Location.X + position + horizontalOffset * 10,
                                                  this.GetStringPosition(stringIndex), isHalfOrLonger);
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

        public void DrawStem(double x, double y0, double y1)
        {
            this.PrimitiveRenderer.DrawStem(this.Location.X + x, this.Location.Y + Math.Min(y0, y1), this.Location.Y + Math.Max(y0, y1));
        }

        public void GetStemOffsetRange(int stringIndex, VoicePart voicePart, out double from, out double to)
        {
            if (voicePart == VoicePart.Treble)
            {
                from = this.GetStringSpacePosition(stringIndex) - this.Style.NoteStemOffset;
                to = Math.Min(from - this.Style.NoteStemHeight,
                               this.GetBodyCeiling() - this.Style.MinimumNoteTailOffset);
            }
            else
            {
                from = this.GetStringSpacePosition(stringIndex + 1) + this.Style.NoteStemOffset;
                to = Math.Max(from + this.Style.NoteStemHeight, this.GetBodyFloor() - this.Style.MinimumNoteTailOffset);
            }

            from -= this.Location.Y;
            to -= this.Location.Y;
        }

        private double GetBodyCeiling() => this.Location.Y + this.Style.BarTopMargin;
        private double GetBodyFloor() => this.GetBodyCeiling() + this.Style.BarLineHeight * this.Style.StringCount;

        private double GetStringPosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 0.5) * this.Style.BarLineHeight;
        private double GetStringSpacePosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + stringIndex * this.Style.BarLineHeight;

        private void DrawHorizontalBarLineTo(int stringIndex, double position)
        {
            this.PrimitiveRenderer.DrawHorizontalBarLine(this.Location.X + this.StringCarets[stringIndex],
                                                         this.GetStringPosition(stringIndex),
                                                         position - this.StringCarets[stringIndex]);
        }

        public void DrawFlag(BaseNoteValue noteValue, double position, int stringIndex, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                return;

            double _, y;
            this.GetStemOffsetRange(stringIndex, voicePart, out _, out y);

            this.PrimitiveRenderer.DrawFlag(noteValue, this.Location.X + position, this.Location.Y + y, voicePart.ToOffBarDirection());
        }


        private double GetBeamOffset(BaseNoteValue noteValue, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                throw new ArgumentException("notes with a base note value longer than eighth can't be beamed",
                                            nameof(noteValue));

            var offset = (BaseNoteValue.Eighth - noteValue)
                          * (this.Style.BeamThickness + this.Style.BeamSpacing)
                          + 0.5 * this.Style.BeamThickness;

            if (voicePart == VoicePart.Treble)
                return offset;
            else
                return -offset;
        }

        public void DrawNoteValueAugment(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, double position, int[] strings, VoicePart voicePart)
        {
            var x = this.Location.X + position + this.Style.NoteValueAugmentOffset;

            var spaceOffset = voicePart == VoicePart.Treble ? -1 : 0;
            foreach (var stringIndex in strings)
            {
                var y = this.GetStringSpacePosition(stringIndex + spaceOffset);
                this.PrimitiveRenderer.DrawNoteValueAugment(noteValueAugment, x, y);
            }
        }

        public void DrawBeam(BaseNoteValue noteValue, double x0, double y0, double x1, double y1, VoicePart voicePart)
        {
            var offset = this.GetBeamOffset(noteValue, voicePart);
            this.PrimitiveRenderer.DrawBeam(x0 + this.Location.X,
                                            y0 + this.Location.Y + offset,
                                            x1 + this.Location.X,
                                            y1 + this.Location.Y + offset);
        }

        public void DrawRest(BaseNoteValue noteValue, double position, VoicePart voicePart)
        {
            var y = voicePart == VoicePart.Treble
                ? this.GetStringSpacePosition(0)   // above the first line
                : this.GetStringSpacePosition(this.Style.StringCount - 1);  // between the 5th and 6th line
            this.PrimitiveRenderer.DrawRest(noteValue, this.Location.X + position, y);
        }


    }
}
