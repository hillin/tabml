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
    class BarRenderingContext : RenderingContextBase<RowRenderingContext>
    {
        public Size AvailableSize { get; }
        public PrimitiveRenderer PrimitiveRenderer => this.Owner.PrimitiveRenderer;
        public TablatureStyle Style => this.Owner.Style;
        public Point Location { get; }
        public BarColumnRenderingInfo[] ColumnRenderingInfos { get; set; }
        public TablatureRenderingContext TablatureRenderingContext => this.Owner.TablatureRenderingContext;
        private readonly Dictionary<Beam, BeamSlope> _beamSlopes;

        public BarRenderingContext(RowRenderingContext owner, Point location, Size availableSize)
            : base(owner)
        {
            this.Location = location;
            this.AvailableSize = availableSize;
            _beamSlopes = new Dictionary<Beam, BeamSlope>();
        }

        public void SetNoteBoundingBox(BarColumn column, int stringIndex, Rect bounds)
        {
            this.ColumnRenderingInfos[column.ColumnIndex].NoteBoundingBoxes[stringIndex] = bounds;
        }

        public Rect? GetNoteBoundingBox(BarColumn column, int stringIndex)
        {
            return this.ColumnRenderingInfos[column.ColumnIndex].NoteBoundingBoxes[stringIndex];
        }

        public BeamSlope GetBeamSlope(IBeatElement beatElement)
        {
            var beam = beatElement?.OwnerBeam;

            if (beam == null)
                return null;

            while (beam.OwnerBeam != null)
                beam = beam.OwnerBeam;

            BeamSlope slope;
            return _beamSlopes.TryGetValue(beam, out slope) ? slope : null;
        }

        public void SetBeamSlope(Beam beam, BeamSlope slope)
        {
            _beamSlopes[beam] = slope;
        }

        public async Task<Rect> DrawFretNumber(int stringIndex, string fretNumber, double position, bool isHalfOrLonger)
        {
            var bounds = await this.PrimitiveRenderer.DrawFretNumber(fretNumber, this.Location.X + position,
                                                                     this.Owner.GetStringPosition(stringIndex),
                                                                     isHalfOrLonger);
            this.UpdateHorizontalBarLine(stringIndex, bounds);

            return bounds;
        }

        private void UpdateHorizontalBarLine(int stringIndex, Rect bounds)
        {
            this.Owner.UpdateHorizontalBarLine(stringIndex,
                                               this.Owner.GetRelativeX(bounds.Left),
                                               this.Owner.GetRelativeX(bounds.Right));
        }

        public async Task<Rect> DrawDeadNote(int stringIndex, double position, bool isHalfOrLonger)
        {
            var bounds = await this.PrimitiveRenderer.DrawDeadNote(this.Location.X + position,
                                                                   this.Owner.GetStringPosition(stringIndex),
                                                                   isHalfOrLonger);

            this.UpdateHorizontalBarLine(stringIndex, bounds);

            return bounds;
        }

        public async Task<Rect> DrawPlayAsChordMark(int stringIndex, double position, bool isHalfOrLonger)
        {
            var bounds = await this.PrimitiveRenderer.DrawPlayToChordMark(this.Location.X + position,
                                                                          this.Owner.GetStringPosition(stringIndex),
                                                                          isHalfOrLonger);

            this.UpdateHorizontalBarLine(stringIndex, bounds);

            return bounds;
        }

        public void DrawBarLine(OpenBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, this.Location.X + position, this.Owner.GetStringPosition(0));
        }

        public void DrawBarLine(CloseBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, this.Location.X + position, this.Owner.GetStringPosition(0));
        }

        public void DrawStem(VoicePart voicePart, double x, double y0, double y1)
        {
            x = this.Location.X + x;
            var yFrom = this.Location.Y + Math.Min(y0, y1);
            var yTo = this.Location.Y + Math.Max(y0, y1);
            this.PrimitiveRenderer.DrawStem(x, yFrom, yTo);

            this.Owner.EnsureHeight(voicePart,
                                    x - this.Style.NoteStemHorizontalMargin,
                                    x + this.Style.NoteStemHorizontalMargin,
                                    y0, y1);
        }

        public double GetNoteAlternationOffset(double offsetRatio)
        {
            return this.Style.NoteAlternationOffset * offsetRatio;
        }

        public void GetStemOffsetRange(int stringIndex, VoicePart voicePart, out double from, out double to)
        {
            if (voicePart == VoicePart.Treble)
            {
                from = this.Owner.GetStringSpacePosition(stringIndex) - this.Style.NoteStemOffset;
                to = Math.Min(from - this.Style.NoteStemHeight,
                               this.Owner.GetBodyCeiling() - this.Style.MinimumNoteTailOffset);
            }
            else
            {
                from = this.Owner.GetStringSpacePosition(stringIndex + 1) + this.Style.NoteStemOffset;
                to = Math.Max(from + this.Style.NoteStemHeight, this.Owner.GetBodyFloor() + this.Style.MinimumNoteTailOffset);
            }

            from -= this.Location.Y;
            to -= this.Location.Y;
        }

        public async Task DrawTuplet(int tuplet, double x, VoicePart voicePart)
        {
            var y = this.Owner.GetHeight(voicePart, x + this.Location.X);

            var bounds = await this.PrimitiveRenderer.DrawTuplet(tuplet.ToString(), x + this.Location.X, y);

            this.Owner.EnsureHeight(voicePart, bounds);
        }

        // x is absolute position
        public async Task DrawGliss(double x, int stringIndex, GlissDirection direction, VoicePart voicePart)
        {
            var bounds = await this.PrimitiveRenderer.DrawGliss(x, this.Owner.GetStringPosition(stringIndex), direction);
            var instructionX = (bounds.Right + bounds.Left) / 2;
            await this.DrawTieInstruction(voicePart, instructionX, "gl.");
        }

        public async Task DrawTieInstruction(VoicePart voicePart, double x, string instruction)
        {
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawTieInstruction(x, y, instruction, voicePart.ToOffBarDirection());
            bounds.Inflate(this.Style.BeatDecoratorMargin, this.Style.BeatDecoratorMargin);
            this.Owner.EnsureHeight(voicePart, bounds);
        }

        public async Task DrawFlag(BaseNoteValue noteValue, double x, double y, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                return;

            var bounds = await this.PrimitiveRenderer.DrawFlag(noteValue,
                                                               this.Location.X + x,
                                                               this.Location.Y + y,
                                                               voicePart.ToOffBarDirection());
            this.Owner.EnsureHeight(voicePart, bounds.Left - this.Style.NoteStemHorizontalMargin, bounds.Right,
                                    bounds.Top, bounds.Bottom);
        }

        private double GetBeamOffset(BaseNoteValue noteValue, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                throw new ArgumentException("notes with a base note value longer than eighth can't be beamed",
                                            nameof(noteValue));

            var offset = (BaseNoteValue.Eighth - noteValue)
                          * (this.Style.BeamThickness + this.Style.BeamSpacing)
                          + 0.5 * this.Style.BeamThickness;

            return voicePart == VoicePart.Treble ? offset : -offset;
        }

        public void DrawNoteValueAugment(NoteValueAugment noteValueAugment, BaseNoteValue noteValue, double position,
                                         int[] strings, VoicePart voicePart)
        {
            var x = this.Location.X + position + this.Style.NoteValueAugmentOffset;

            var spaceOffset = voicePart == VoicePart.Treble ? -1 : 0;
            foreach (var stringIndex in strings)
            {
                var y = this.Owner.GetStringSpacePosition(stringIndex + spaceOffset);
                this.PrimitiveRenderer.DrawNoteValueAugment(noteValueAugment, x, y);
            }
        }

        public void DrawBeam(BaseNoteValue noteValue, double x0, double y0, double x1, double y1, VoicePart voicePart)
        {
            var offset = this.GetBeamOffset(noteValue, voicePart);
            x0 = x0 + this.Location.X;
            y0 = y0 + this.Location.Y + offset;
            x1 = x1 + this.Location.X;
            y1 = y1 + this.Location.Y + offset;
            this.PrimitiveRenderer.DrawBeam(x0, y0, x1, y1);
            this.Owner.EnsureHeightSloped(voicePart, x0, x1, y0, y1, this.Style.NoteTailVerticalMargin);
        }

        public void DrawRest(BaseNoteValue noteValue, double position, VoicePart voicePart)
        {
            var y = voicePart == VoicePart.Treble
                ? this.Owner.GetStringSpacePosition(0)   // above the first line
                : this.Owner.GetStringSpacePosition(this.Style.StringCount - 1);  // between the 5th and 6th line
            this.PrimitiveRenderer.DrawRest(noteValue, this.Location.X + position, y);
        }


        public Task<Rect> MeasureRest(BaseNoteValue noteValue)
        {
            return this.PrimitiveRenderer.MeasureRest(noteValue);
        }
    }
}
