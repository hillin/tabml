using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    class BarRenderer : ElementRenderer<Bar, RowRenderingContext>
    {
        public TablatureStyle Style { get; }

        private double? _minSize;

        private readonly List<BarColumnRenderer> _columnRenderers;
        private readonly List<BarVoiceRenderer> _voiceRenderers;

        private BarRenderingContext _barRenderingContext;

        public BarRenderer(TablatureRenderer owner, TablatureStyle style, Bar bar)
            : base(owner, bar)
        {
            this.Style = style;

            _columnRenderers = new List<BarColumnRenderer>();
            _voiceRenderers = new List<BarVoiceRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _columnRenderers.AddRange(this.Element.Columns.Select(c => new BarColumnRenderer(this, c)));

            _columnRenderers.Initialize();

            if (this.Element.BassVoice != null)
                _voiceRenderers.Add(new BarVoiceRenderer(this, this.Element.BassVoice));

            if (this.Element.TrebleVoice != null)
                _voiceRenderers.Add(new BarVoiceRenderer(this, this.Element.TrebleVoice));

            _voiceRenderers.Initialize();
        }

        public double GetMinSize()
        {
            if (_minSize == null)
                throw new InvalidOperationException("min size is not measured yet, call MeasureMinSize first");

            return _minSize.Value;
        }


        public async Task<double> MeasureMinSize(PrimitiveRenderer primitiveRenderer)
        {
            if (_minSize != null)
                return _minSize.Value;

            var minDuration = this.Element.Columns.Min(c => c.GetDuration());
            var size = 0.0;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var column in this.Element.Columns)
                size += await this.GetColumnMinWidthInBar(primitiveRenderer, column, minDuration);

            _minSize = size;

            return _minSize.Value;
        }

        private async Task<double> GetColumnMinWidthInBar(PrimitiveRenderer primitiveRenderer, BarColumn column, PreciseDuration minDurationInBar)
        {
            var columnRegularWidth = Math.Min(this.Style.MaximumBeatSizeWithoutLyrics,
                                              this.Style.MinimumBeatSize * column.GetDuration() / minDurationInBar);

            double columnMinWidth;
            if (column.Lyrics == null)
                columnMinWidth = this.Style.MinimumBeatSize;
            else
            {
                var lyricsBounds = await primitiveRenderer.MeasureLyrics(column.Lyrics.Text);
                columnMinWidth = Math.Max(this.Style.MinimumBeatSize, lyricsBounds.Width);
            }

            return Math.Max(columnRegularWidth, columnMinWidth);
        }

        public async Task Render(Point location, Size size, bool isFirstBarInRow)
        {

            _barRenderingContext = new BarRenderingContext(this.RenderingContext, location, size, isFirstBarInRow);
            _columnRenderers.AssignRenderingContexts(_barRenderingContext);
            _voiceRenderers.AssignRenderingContexts(_barRenderingContext);

            var width = size.Width;
            if (this.Element.OpenLine != null)
                _barRenderingContext.DrawOpenBarLine(this.Element.OpenLine.Value, 0.0);

            var position = 0.0;
            if (isFirstBarInRow)
                position += await _barRenderingContext.DrawTabHeader();

            position += _barRenderingContext.Style.BarHorizontalPadding;

            position += await this.RenderTimeSignature(_barRenderingContext, position);

            var minDuration = this.Element.Columns.Min(c => c.GetDuration());
            var minSize = await this.MeasureMinSize(this.RenderingContext.PrimitiveRenderer);
            var widthRatio = (width - position) / minSize;

            _barRenderingContext.ColumnRenderingInfos = new BarColumnRenderingInfo[this.Element.Columns.Count];

            for (var i = 0; i < this.Element.Columns.Count; i++)
            {
                var column = this.Element.Columns[i];

                var columnWidth = await this.GetColumnMinWidthInBar(this.RenderingContext.PrimitiveRenderer, column, minDuration) * widthRatio;
                _barRenderingContext.ColumnRenderingInfos[i] = new BarColumnRenderingInfo(column, position, columnWidth);

                var barColumnRenderer = _columnRenderers[i];
                await barColumnRenderer.PreRender();
                position += columnWidth;
            }

            foreach (var renderer in _voiceRenderers)
                await renderer.Render();

            foreach (var barColumnRenderer in _columnRenderers)
                await barColumnRenderer.Render();

            if (this.Element.CloseLine != null)
                _barRenderingContext.DrawCloseBarLine(this.Element.CloseLine.Value, width);

            if (this.Element.AlternativeEndingPosition == AlternativeEndingPosition.Start)
                _barRenderingContext.IsSectionRenderingPostponed = true;
            else
                await this.RenderSection();

            await this.RenderTranspositionSignature();
            await this.RenderTempoSignature();

            this.RenderingContext.PreviousDocumentState = this.Element.DocumentState;
        }

        private async Task RenderTempoSignature()
        {
            var previousDocumentState = this.RenderingContext.PreviousDocumentState;
            if (previousDocumentState == null ||
                previousDocumentState.TempoSignature != this.Element.DocumentState.TempoSignature)
            {
                await _barRenderingContext.DrawTempoSignature(this.Element.DocumentState.TempoSignature.Tempo, 0);
            }
        }

        private async Task RenderTranspositionSignature()
        {
            var previousDocumentState = this.RenderingContext.PreviousDocumentState;
            if (previousDocumentState != null &&
                previousDocumentState.KeySignature != this.Element.DocumentState.KeySignature)
            {
                await _barRenderingContext.DrawTransposition(this.Element.DocumentState.KeySignature.Key, 0);
            }
        }

        private async Task<double> RenderTimeSignature(BarRenderingContext renderingContext, double position)
        {
            var previousDocumentState = this.RenderingContext.PreviousDocumentState;
            if (previousDocumentState == null || previousDocumentState.Time != this.Element.DocumentState.Time)
            {
                return await renderingContext.DrawTimeSignature(this.Element.DocumentState.Time, position);
            }

            return 0.0;
        }

        public async Task PostRender()
        {
            await this.RenderAlternation();

            if (_barRenderingContext.IsSectionRenderingPostponed)
                await this.RenderSection();

            foreach (var barColumnRenderer in _columnRenderers)
                await barColumnRenderer.PostRender();

            this.RenderingContext.PreviousDocumentState = this.Element.DocumentState;
        }

        private async Task RenderAlternation()
        {

            switch (this.Element.AlternativeEndingPosition)
            {
                case AlternativeEndingPosition.Start:
                    await _barRenderingContext.DrawStartAlternation(this.Element.DocumentState.CurrentAlternation.GetFormattedIndices());
                    break;
                case AlternativeEndingPosition.Inside:
                    if (this.RenderingContext.Style.DrawFullAlternationEnding)
                        await _barRenderingContext.DrawAlternationLine();
                    break;
                case AlternativeEndingPosition.End:
                    if (this.RenderingContext.Style.DrawFullAlternationEnding)
                        await _barRenderingContext.DrawEndAlternation();
                    break;
                case AlternativeEndingPosition.StartAndEnd:
                    await
                        _barRenderingContext.DrawStartAndEndAlternation(this.Element.DocumentState.CurrentAlternation.GetFormattedIndices());
                    break;
            }
        }

        private async Task RenderSection()
        {
            var previousDocumentState = this.RenderingContext.PreviousDocumentState;
            if (this.Element.DocumentState.CurrentSection != null)
            {
                if (previousDocumentState == null ||
                    this.Element.DocumentState.CurrentSection != previousDocumentState.CurrentSection)
                {
                    await _barRenderingContext.DrawSection(this.Element.DocumentState.CurrentSection.Name);
                }
            }
        }

    }
}
