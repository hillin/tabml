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

            this.StringCarets = new double[style.StringCount];
        }

        public void DrawFretNumber(int stringIndex, string fretNumber, double position, bool isHalfOrLonger)
        {
            this.DrawHorizontalBarLineTo(stringIndex, position - 10); // todo: use measure string to handle spaces

            this.StringCarets[stringIndex] = position + 10;

            this.PrimitiveRenderer.DrawFretNumber(fretNumber, this.Location.X + position,
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

        private double OffsetByVoicePart(double baseValue, double offset, VoicePart voicePart) => voicePart == VoicePart.Treble ? baseValue - offset : baseValue + offset;

        private double GetOffbarPosition(double offset, VoicePart voicePart) => voicePart == VoicePart.Treble ? this.GetBodyCeiling() - offset : this.GetBodyFloor() + offset;

        public void DrawStem(BaseNoteValue noteValue, double position, VoicePart voicePart)
        {
            if (noteValue >= BaseNoteValue.Whole)
                return;

            var tailOffset = noteValue == BaseNoteValue.Half
                ? this.Style.NoteStemOffset + (this.Style.NoteTailOffset - this.Style.NoteStemOffset) / 2
                : this.Style.NoteTailOffset;

            var yFrom = this.GetOffbarPosition(this.Style.NoteStemOffset, voicePart);
            var yTo = this.GetOffbarPosition(tailOffset, voicePart);

            this.PrimitiveRenderer.DrawStem(this.Location.X + position, Math.Min(yFrom, yTo), Math.Max(yFrom, yTo));
        }

        private double GetBodyCeiling() => this.Location.Y + this.Style.BarTopMargin;
        private double GetBodyFloor() => this.GetBodyCeiling() + this.Style.BarLineHeight * this.Style.StringCount;

        private double GetStringPosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 0.5) * this.Style.BarLineHeight;
        private double GetStringSpacePosition(int stringIndex) => this.Location.Y + this.Style.BarTopMargin + (stringIndex + 1) * this.Style.BarLineHeight;

        private void DrawHorizontalBarLineTo(int stringIndex, double position)
        {
            this.PrimitiveRenderer.DrawHorizontalBarLine(this.Location.X + this.StringCarets[stringIndex],
                                                         this.GetStringPosition(stringIndex),
                                                         position - this.StringCarets[stringIndex]);
        }

        public void DrawFlag(BaseNoteValue noteValue, double position, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                return;

            this.PrimitiveRenderer.DrawFlag(noteValue, this.Location.X + position,
                                            this.GetOffbarPosition(this.Style.NoteTailOffset, voicePart), voicePart.ToOffBarDirection());
        }

        private double GetBeamYPosition(BaseNoteValue noteValue, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                throw new ArgumentException("notes with a base note value longer than eighth can't be beamed",
                                            nameof(noteValue));

            var offset = this.Style.NoteTailOffset -
                         (BaseNoteValue.Eighth - noteValue) * (this.Style.BeamThickness + this.Style.BeamSpacing)
                         - 0.5 * this.Style.BeamThickness;

            return this.GetOffbarPosition(offset, voicePart);
        }

        public void DrawSemiBeam(BaseNoteValue noteValue, double position, VoicePart voicePart, bool isLastOfBeam)
        {
            var xFrom = this.Location.X + position;
            var xTo = isLastOfBeam ? xFrom - this.Style.HalfBeamWidth : xFrom + this.Style.HalfBeamWidth;

            var y = this.GetBeamYPosition(noteValue, voicePart);
            this.PrimitiveRenderer.DrawBeam(Math.Min(xFrom, xTo), y, Math.Max(xFrom, xTo), y);
        }

        public void DrawNoteValueAugment(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, int[] strings, double position, VoicePart voicePart)
        {
            var x = this.Location.X + position + this.Style.NoteValueAugmentOffset;

            var spaceOffset = voicePart == VoicePart.Treble ? -1 : 0;
            foreach (var stringIndex in strings)
            {
                var y = this.GetStringSpacePosition(stringIndex - 1 + spaceOffset);
                this.PrimitiveRenderer.DrawNoteValueAugment(noteValueAugment, x, y);
            }
        }

        public void DrawNoteValueAugmentOnBeam(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, double position,
                                               VoicePart voicePart)
        {
            var y = this.GetBeamYPosition(noteValue.Half(), voicePart);
            var x = this.Location.X + position + this.Style.NoteValueAugmentOffset;

            this.PrimitiveRenderer.DrawNoteValueAugment(noteValueAugment, x, y);
        }

        public void DrawBeam(BaseNoteValue noteValue, double @from, double to, VoicePart voicePart)
        {
            var y = this.GetBeamYPosition(noteValue, voicePart);
            this.PrimitiveRenderer.DrawBeam(@from + this.Location.X, y, to + this.Location.X, y);
        }

        public void DrawRest(BaseNoteValue noteValue, double position, VoicePart voicePart)
        {
            var y = voicePart == VoicePart.Bass ? this.GetStringSpacePosition(0) : this.GetStringSpacePosition(this.Style.StringCount - 1);
            this.PrimitiveRenderer.DrawRest(noteValue, this.Location.X + position, y);
        }
    }
}
