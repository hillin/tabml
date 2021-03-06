using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public BarInRowPosition InRowPosition { get; }
        public PrimitiveRenderer PrimitiveRenderer => this.Owner.PrimitiveRenderer;
        public TablatureStyle Style => this.Owner.Style;
        public Point Location { get; }
        public BarColumnRenderingInfo[] ColumnRenderingInfos { get; set; }
        public TablatureRenderingContext TablatureRenderingContext => this.Owner.TablatureRenderingContext;

        public bool IsSectionRenderingPostponed { get; set; }

        private readonly Dictionary<Beam, BeamSlope> _beamSlopes;


        public BarRenderingContext(RowRenderingContext owner, Point location, Size availableSize, BarInRowPosition inRowPosition)
            : base(owner)
        {
            this.Location = location;
            this.AvailableSize = availableSize;
            this.InRowPosition = inRowPosition;
            _beamSlopes = new Dictionary<Beam, BeamSlope>();
        }

        public void SetNoteBoundingBox(int columnIndex, int stringIndex, Rect bounds)
        {
            this.ColumnRenderingInfos[columnIndex].NoteBoundingBoxes[stringIndex] = bounds;
        }

        public Rect? GetNoteBoundingBox(int columnIndex, int stringIndex)
        {
            return this.ColumnRenderingInfos[columnIndex].NoteBoundingBoxes[stringIndex];
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

        public async Task<Rect> DrawNoteFretting(int stringIndex, string fretting, double position, NoteRenderingFlags flags)
        {
            var bounds = await this.PrimitiveRenderer.DrawNoteFretting(fretting, this.Location.X + position,
                                                                     this.Owner.GetStringPosition(stringIndex),
                                                                     flags);
            this.UpdateHorizontalBarLine(stringIndex, bounds);

            return bounds;
        }

        private void UpdateHorizontalBarLine(int stringIndex, Rect bounds)
        {
            this.Owner.UpdateHorizontalBarLine(stringIndex,
                                               this.Owner.GetRelativeX(bounds.Left),
                                               this.Owner.GetRelativeX(bounds.Right));
        }


        public void DrawOpenBarLine(OpenBarLine line, double position)
        {
            this.PrimitiveRenderer.DrawBarLine((BarLine)line, this.Location.X + position, this.Owner.GetStringPosition(0));
        }

        public void DrawCloseBarLine(CloseBarLine line, double position)
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
                                    this.Owner.Location.Y + y0,
                                    this.Owner.Location.Y + y1,
                                    this.Style.NoteTailVerticalMargin);
        }


        public double GetNoteAlternationOffset(double offsetRatio, bool hasHarmonics)
        {
            return (hasHarmonics ? this.Style.NoteAlternationOffsetWithHarmonics : this.Style.NoteAlternationOffset) * offsetRatio;
        }

        public void GetStemOffsetRange(int columnIndex, int stringIndex, VoicePart voicePart, out double from, out double to)
        {
            var noteBounds = this.GetNoteBoundingBox(columnIndex, stringIndex);
            if (voicePart == VoicePart.Treble)
            {
                var baseFrom = noteBounds?.Top ?? this.Owner.GetStringSpacePosition(stringIndex);
                from = baseFrom - this.Style.NoteStemOffset;
                to = Math.Min(from - this.Style.NoteStemHeight,
                               this.Owner.GetBodyCeiling() - this.Style.MinimumNoteTailOffset);
            }
            else
            {
                var baseFrom = noteBounds?.Bottom ?? this.Owner.GetStringSpacePosition(stringIndex + 1);
                from = baseFrom + this.Style.NoteStemOffset;
                to = Math.Max(from + this.Style.NoteStemHeight, this.Owner.GetBodyFloor() + this.Style.MinimumNoteTailOffset);
            }

            from -= this.Location.Y;
            to -= this.Location.Y;
        }

        public async Task DrawTuplet(int tuplet, double x, VoicePart voicePart)
        {
            var y = this.Owner.GetHeight(voicePart, x + this.Location.X);

            var bounds = await this.PrimitiveRenderer.DrawTuplet(tuplet, x + this.Location.X, y);

            this.Owner.EnsureHeight(voicePart, bounds);
        }

        // x is absolute position
        public Task<Rect> DrawGliss(double x, int stringIndex, GlissDirection direction, VoicePart voicePart)
        {
            return this.PrimitiveRenderer.DrawGliss(x, this.Owner.GetStringPosition(stringIndex), direction);
        }

        /// <remarks><paramref name="x"/> must be an absolute position</remarks>
        public async Task DrawConnectionInstruction(VoicePart voicePart, double x, string instruction)
        {
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawConnectionInstruction(x, y, instruction, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        public async Task DrawTransposition(NoteName key, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(VoicePart.Treble, x);
            var bounds = await this.PrimitiveRenderer.DrawTranspositionText(x, y, key.ToString());
            this.EnsureHeightForOrnament(VoicePart.Treble, bounds);
        }

        public async Task DrawTempoSignature(Tempo tempo, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(VoicePart.Treble, x);
            var bounds = await this.PrimitiveRenderer.DrawTempoSignature(x, y, tempo.NoteValue, tempo.Beats);
            this.EnsureHeightForOrnament(VoicePart.Treble, bounds);
        }

        public async Task DrawArtificialHarmonicText(VoicePart voicePart, double x, string text)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawArtificialHarmonicText(x, y, text, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        private void EnsureHeightForOrnament(VoicePart voicePart, Rect bounds)
        {
            bounds.Inflate(this.Style.BeatOrnamentMargin, this.Style.BeatOrnamentMargin);
            this.Owner.EnsureHeight(voicePart, bounds);
        }


        public async Task DrawFlag(BaseNoteValue noteValue, double x, double y, VoicePart voicePart)
        {
            if (noteValue > BaseNoteValue.Eighth)
                return;

            var bounds = await this.PrimitiveRenderer.DrawFlag(noteValue,
                                                               this.Location.X + x,
                                                               this.Location.Y + y,
                                                               voicePart.ToDirection());
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

        public void DrawNoteValueAugment(NoteValue noteValue, double position, double stringIndex)
        {
            var x = this.Location.X + position + this.Style.NoteValueAugmentOffset;
            var y = this.Owner.GetStringSpacePosition(stringIndex);
            this.PrimitiveRenderer.DrawNoteValueAugment(noteValue.Augment, x, y);
        }

        public async Task DrawBeam(BaseNoteValue noteValue, double x0, double y0, double x1, double y1, VoicePart voicePart)
        {
            var offset = this.GetBeamOffset(noteValue, voicePart);
            x0 = x0 + this.Location.X;
            y0 = y0 + this.Location.Y + offset;
            x1 = x1 + this.Location.X;
            y1 = y1 + this.Location.Y + offset;
            var bounds = await this.PrimitiveRenderer.DrawBeam(x0, y0, x1, y1);

            var beamHalfThickness = voicePart == VoicePart.Treble
                ? Math.Min(y0, y1) - bounds.Top
                : bounds.Bottom - Math.Max(y0, y1);

            this.Owner.EnsureHeightSloped(voicePart, x0, x1, y0, y1, this.Style.NoteTailVerticalMargin + beamHalfThickness, this.Style.NoteStemHorizontalMargin);
        }

        public Task<Rect> DrawRest(BaseNoteValue noteValue, double position, double stringIndex)
        {
            return this.PrimitiveRenderer.DrawRest(noteValue, this.Location.X + position, this.Owner.GetStringPosition(stringIndex));
        }

        public Task<Rect> MeasureRest(BaseNoteValue noteValue)
        {
            return this.PrimitiveRenderer.MeasureRest(noteValue);
        }

        public async Task DrawBeatModifier(BeatModifier modifier, VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawBeatModifier(x, y, modifier, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        public async Task DrawRasgueadoText(VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawRasgueadoText(x, y, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }
        
        public async Task DrawTremolo(VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawTremolo(x, y, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        public async Task DrawBrushUp(VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawBrushUp(x, y, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        public async Task DrawBrushDown(VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawBrushDown(x, y, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        public async Task DrawArpeggioUp(VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawArpeggioUp(x, y, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        public async Task DrawArpeggioDown(VoicePart voicePart, double x)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(voicePart, x);
            var bounds = await this.PrimitiveRenderer.DrawArpeggioDown(x, y, voicePart.ToDirection());
            this.EnsureHeightForOrnament(voicePart, bounds);
        }

        private async Task<double> DrawInlineBrushlikeTechnique(int columnIndex, int minString, int maxString,
                                                                Func<double, double, int, Task<Rect>> primitiveMethod)
        {
            var y = this.Owner.GetStringPosition(((double)minString + maxString) / 2.0);

            var x = this.ColumnRenderingInfos[columnIndex].Position + this.Location.X;
            var bounds = await primitiveMethod(x, y, maxString - minString + 1);

            for (var i = minString; i <= maxString; ++i)
                this.SetNoteBoundingBox(columnIndex, i, bounds);

            return bounds.Width;
        }

        public Task<double> DrawInlineBrushDown(int columnIndex, int minString, int maxString)
        {
            return this.DrawInlineBrushlikeTechnique(columnIndex, minString, maxString, this.PrimitiveRenderer.DrawInlineBrushDown);
        }

        public Task<double> DrawInlineBrushUp(int columnIndex, int minString, int maxString)
        {
            return this.DrawInlineBrushlikeTechnique(columnIndex, minString, maxString, this.PrimitiveRenderer.DrawInlineBrushUp);
        }

        public Task<double> DrawInlineArpeggioDown(int columnIndex, int minString, int maxString)
        {
            return this.DrawInlineBrushlikeTechnique(columnIndex, minString, maxString, this.PrimitiveRenderer.DrawInlineArpeggioDown);
        }

        public Task<double> DrawInlineArpeggioUp(int columnIndex, int minString, int maxString)
        {
            return this.DrawInlineBrushlikeTechnique(columnIndex, minString, maxString, this.PrimitiveRenderer.DrawInlineArpeggioUp);
        }

        public Task<double> DrawInlineRasgueado(int columnIndex, int minString, int maxString)
        {
            return this.DrawInlineBrushlikeTechnique(columnIndex, minString, maxString, this.PrimitiveRenderer.DrawInlineRasgueado);
        }
        public async Task DrawLyrics(double x, string lyrics)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(VoicePart.Bass, x);

            await this.PrimitiveRenderer.DrawLyrics(lyrics, x, y);
        }

        public async Task DrawChord(double x, IChordDefinition chord)
        {
            x += this.Location.X;
            var y = this.Owner.GetHeight(VoicePart.Treble, x);
            await this.PrimitiveRenderer.DrawChord(chord, x, y);
        }

        public async Task<double> DrawTimeSignature(Time time, double x)
        {
            var bounds =
                await this.PrimitiveRenderer.DrawTimeSignature(x + this.Location.X,
                                                               this.Owner.GetBodyCenter(),
                                                               time.Beats,
                                                               time.NoteValue.GetInvertedDuration());

            return bounds.Width;
        }


        public async Task<double> DrawTabHeader()
        {
            var bounds =
                await this.PrimitiveRenderer.DrawTabHeader(this.Location.X, this.Owner.GetBodyCenter());

            return bounds.Width;
        }

        public async Task DrawSection(string currentSectionName)
        {
            var y = this.Owner.GetHeight(VoicePart.Treble, this.Location.X);
            var bounds = await this.PrimitiveRenderer.DrawSection(this.Location.X, y, currentSectionName);
            this.EnsureHeightForOrnament(VoicePart.Treble, bounds);
        }

        public async Task DrawStartAlternation(string text)
        {

            var bounds = await this.PrimitiveRenderer.DrawStartAlternation(
                                       this.Location.X,
                                       this.Location.X + this.AvailableSize.Width,
                                       this.Owner.GetBodyCeiling(),
                                       this.Owner.GetHeight(VoicePart.Treble, this.Location.X),
                                       text);
            this.Owner.EnsureHeight(VoicePart.Treble, bounds);
        }

        public async Task DrawAlternationLine()
        {
            var bounds = await this.PrimitiveRenderer.DrawAlternationLine(
                                       this.Location.X,
                                       this.Location.X + this.AvailableSize.Width,
                                       this.Owner.GetHeight(VoicePart.Treble, this.Location.X));

            this.Owner.EnsureHeight(VoicePart.Treble, bounds);
        }

        public async Task DrawEndAlternation()
        {
            var bounds = await this.PrimitiveRenderer.DrawEndAlternation(
                                       this.Location.X,
                                       this.Location.X + this.AvailableSize.Width,
                                       this.Owner.GetBodyCeiling(),
                                       this.Owner.GetHeight(VoicePart.Treble, this.Location.X));
            this.Owner.EnsureHeight(VoicePart.Treble, bounds);
        }

        public async Task DrawStartAndEndAlternation(string text)
        {
            var bounds = await this.PrimitiveRenderer.DrawStartAndEndAlternation(
                                       this.Location.X,
                                       this.Location.X + this.AvailableSize.Width,
                                       this.Owner.GetBodyCeiling(),
                                       this.Owner.GetHeight(VoicePart.Treble, this.Location.X),
                                       text);
            this.Owner.EnsureHeight(VoicePart.Treble, bounds);
        }

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public async Task DrawEllipseAroundNotes(int columnIndex, int[] strings)
        {
            var bounds = this.GetNoteBoundingBox(columnIndex, strings[0]).Value;
            var width = Math.Max(bounds.Width, bounds.Height);

            for (var i = 1; i < strings.Length; ++i)
            {
                var currentBounds = this.GetNoteBoundingBox(columnIndex, strings[i]).Value;
                bounds.Union(currentBounds);
                width = Math.Max(width, Math.Max(currentBounds.Width, currentBounds.Height));
            }

            if (bounds.Width < width)
            {
                bounds.X -= (width - bounds.Width) / 2;
                bounds.Width = width;
            }

            var ellipseBounds = await this.PrimitiveRenderer.DrawEllipseAroundNotes(bounds);
            for (var stringIndex = strings.Min(); stringIndex <= strings.Max(); ++stringIndex)
                this.UpdateHorizontalBarLine(stringIndex, ellipseBounds);
        }
    }
}
