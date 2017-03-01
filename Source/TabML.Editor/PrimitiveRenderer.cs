using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Core.Style;
using TabML.Editor.Rendering;
using Rect = System.Windows.Rect;

namespace TabML.Editor
{
    class PrimitiveRenderer
    {
        class JsonString
        {
            public string Json { get; }
            public JsonString(string json)
            {
                this.Json = json;
            }
        }


        private readonly ChromiumWebBrowser _browser;
        private IFrame BrowserMainFrame => _browser.GetBrowser().MainFrame;

        public PrimitiveRenderer(ChromiumWebBrowser browser)
        {
            _browser = browser;
        }

        private static string CreateJavascriptCall(string method, params object[] args)
        {
            var builder = new StringBuilder();
            builder.Append("renderer.")
                   .Append(method)
                   .Append("(")
                   .Append(string.Join(", ", args.Select(PrimitiveRenderer.FormatArg)))
                   .Append(")");

            Debug.WriteLine(builder.ToString());

            return builder.ToString();
        }

        private void InvokeRenderMethod(string method, params object[] args)
        {
            this.BrowserMainFrame.ExecuteJavaScriptAsync(PrimitiveRenderer.CreateJavascriptCall(method, args));
        }


        private async Task<Rect> InvokeRenderMethodReturnBoundingBox(string method, params object[] args)
        {
            var task = await this.BrowserMainFrame.EvaluateScriptAsync(PrimitiveRenderer.CreateJavascriptCall(method, args));
            var result = (Dictionary<string, object>)task.Result;
            return PrimitiveRenderer.CreateBoundingBox(result);
        }

        private async Task<Rect> InvokeAsyncRenderMethodReturnBoundingBox(string method, params object[] args)
        {
            await BrowserContext.CallbackObject.Acquire();

            await this.BrowserMainFrame.EvaluateScriptAsync(PrimitiveRenderer.CreateJavascriptCall(method, args));

            SpinWait.SpinUntil(() => BrowserContext.CallbackObject.IsCalledBack);

            var result = BrowserContext.CallbackObject.Result;
            BrowserContext.CallbackObject.Release();

            return PrimitiveRenderer.CreateBoundingBox(result);
        }

        private static Rect CreateBoundingBox(Dictionary<string, object> result)
        {
            return new Rect(Convert.ToDouble(result["left"]), Convert.ToDouble(result["top"]),
                            Convert.ToDouble(result["width"]), Convert.ToDouble(result["height"]));
        }

        private static string FormatArg(object arg)
        {
            var jsonArg = arg as JsonString;
            if (jsonArg != null)
                return jsonArg.Json;

            if (arg is string)
                return $"\"{arg}\"";

            if (arg is bool)
            {
                if ((bool)arg)
                    return "true";

                return "false";
            }

            if (arg == null)
                return "null";

            if (arg is Enum)
                // ReSharper disable once PossibleInvalidCastException
                return ((int)arg).ToString();

            return arg.ToString();
        }



        public Task<Rect> DrawTitle(string title, double x, double y)
            => this.InvokeRenderMethodReturnBoundingBox("drawTitle", title, x, y);
        public Task<Rect> DrawLyrics(string lyrics, double x, double y)
            => this.InvokeRenderMethodReturnBoundingBox("drawLyrics", lyrics, x, y);
        public Task<Rect> MeasureLyrics(string lyrics)
            => this.InvokeRenderMethodReturnBoundingBox("measureLyrics", lyrics);
        public Task<Rect> DrawNoteFretting(string fretting, double x, double y, NoteRenderingFlags flags)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawNoteFretting", fretting, x, y, flags);
        public void DrawHorizontalBarLine(double x, double y, double length)
            => this.InvokeRenderMethod("drawHorizontalBarLine", x, y, length);
        public void DrawBarLine(BarLine line, double x, double y)
            => this.InvokeRenderMethod("drawBarLine", (int)line, x, y);
        public void DrawStem(double x, double yFrom, double yTo)
            => this.InvokeRenderMethod("drawStem", x, yFrom, yTo);
        public Task<Rect> DrawFlag(BaseNoteValue noteValue, double x, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawFlag", (int)noteValue, x, y, (int)direction);
        public Task<Rect> DrawBeam(double x1, double y1, double x2, double y2)
            => this.InvokeRenderMethodReturnBoundingBox("drawBeam", x1, y1, x2, y2);
        public void DrawNoteValueAugment(NoteValueAugment augment, double x, double y)
            => this.InvokeRenderMethod("drawNoteValueAugment", (int)augment, x, y);
        public Task<Rect> DrawRest(BaseNoteValue noteValue, double x, double y)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawRest", (int)noteValue, x, y);
        public Task<Rect> MeasureRest(BaseNoteValue noteValue)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("measureRest", noteValue);
        public Task<Rect> DrawTuplet(int tuplet, double x, double y)
            => this.InvokeRenderMethodReturnBoundingBox("drawTuplet", tuplet, x, y);
        public Task<Rect> DrawTie(double x0, double x1, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawTie", x0, x1, y, (int)direction);
        public Task<Rect> DrawGliss(double x, double y, GlissDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawGliss", x, y, direction);
        public Task<Rect> DrawConnectionInstruction(double x, double y, string instruction, VerticalDirection direction)
            => this.InvokeRenderMethodReturnBoundingBox("drawConnectionInstruction", x, y, instruction, direction);
        public Task<Rect> DrawArtificialHarmonicText(double x, double y, string text, VerticalDirection direction)
            => this.InvokeRenderMethodReturnBoundingBox("drawArtificialHarmonicText", x, y, text, direction);
        public Task<Rect> DrawRasgueadoText(double x, double y, VerticalDirection direction)
            => this.InvokeRenderMethodReturnBoundingBox("drawRasgueadoText", x, y, direction);

        public Task<Rect> DrawBeatModifier(double x, double y, BeatModifier modifer, VerticalDirection direction)
            => this.InvokeRenderMethodReturnBoundingBox("drawBeatModifier", x, y, modifer, direction);
        
        public Task<Rect> DrawTremolo(double x, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawTremolo", x, y, direction);

        public Task<Rect> DrawBrushUp(double x, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawBrushUp", x, y, direction);

        public Task<Rect> DrawBrushDown(double x, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawBrushDown", x, y, direction);

        public Task<Rect> DrawArpeggioUp(double x, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawArpeggioUp", x, y, direction);

        public Task<Rect> DrawArpeggioDown(double x, double y, VerticalDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawArpeggioDown", x, y, direction);

        public Task<Rect> DrawInlineBrushDown(double x, double y, int stringSpan)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawInlineBrushDown", x, y, stringSpan);

        public Task<Rect> DrawInlineBrushUp(double x, double y, int stringSpan)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawInlineBrushUp", x, y, stringSpan);

        public Task<Rect> DrawInlineArpeggioDown(double x, double y, int stringSpan)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawInlineArpeggioDown", x, y, stringSpan);

        public Task<Rect> DrawInlineArpeggioUp(double x, double y, int stringSpan)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawInlineArpeggioUp", x, y, stringSpan);

        public Task<Rect> DrawInlineRasgueado(double x, double y, int stringSpan)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawInlineRasgueado", x, y, stringSpan);

        public Task<Rect> DrawTimeSignature(double x, double y, int beats, int timeValue)
            => this.InvokeRenderMethodReturnBoundingBox("drawTimeSignature", x, y, beats, timeValue);

        public Task<Rect> DrawTranspositionText(double x, double y, string key)
            => this.InvokeRenderMethodReturnBoundingBox("drawTranspositionText", x, y, key);

        public Task<Rect> DrawTempoSignature(double x, double y, BaseNoteValue noteValue, int beats)
            => this.InvokeRenderMethodReturnBoundingBox("drawTempoSignature", x, y, noteValue, beats);

        public Task<Rect> DrawTabHeader(double x, double y)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawTabHeader", x, y);

        public Task<Rect> DrawSection(double x, double y, string sectionName)
            => this.InvokeRenderMethodReturnBoundingBox("drawSection", x, y, sectionName);

        public Task<Rect> DrawStartAlternation(double x0, double x1, double y0, double y1, string text)
            => this.InvokeRenderMethodReturnBoundingBox("drawStartAlternation", x0, x1, y0, y1, text);

        public Task<Rect> DrawStartAndEndAlternation(double x0, double x1, double y0, double y1, string text)
            => this.InvokeRenderMethodReturnBoundingBox("drawStartAndEndAlternation", x0, x1, y0, y1, text);

        public Task<Rect> DrawAlternationLine(double x0, double x1, double y1)
            => this.InvokeRenderMethodReturnBoundingBox("drawAlternationLine", x0, x1, y1);

        public Task<Rect> DrawEndAlternation(double x0, double x1, double y0, double y1)
            => this.InvokeRenderMethodReturnBoundingBox("drawEndAlternation", x0, x1, y0, y1);

        public void Clear() => this.InvokeRenderMethod("clear");

        public void DebugDrawHeightMap(IEnumerable<Point> points)
        {
            var builder = new StringBuilder();
            builder.Append("renderer.debugDrawHeightMap( [ ");

            foreach (var point in points)
            {
                builder.Append($"{{ x: {point.X}, y: {point.Y} }},");
            }

            builder.Remove(builder.Length - 1, 1);  // remove last comma

            builder.Append(" ] )");

            Debug.WriteLine(builder.ToString());

            this.BrowserMainFrame.ExecuteJavaScriptAsync(builder.ToString());
        }

        public Task<Rect> DrawChord(IChordDefinition chord, double x, double y)
        {
            var name = chord.DisplayName;
            var fingering = "null";

            if (chord.Fingering != null)
            {
                var builder = new StringBuilder();

                builder.Append('[');

                foreach (var note in chord.Fingering.Notes)
                {
                    switch (note.Fret)
                    {
                        case ChordFingeringNote.FingeringSkipString:
                            builder.Append("'x'");
                            break;
                        case 0:
                            builder.Append("0");
                            break;
                        default:

                            builder.Append('{');

                            builder.Append("fret:")
                                   .Append(note.Fret)
                                   .Append(',');

                            if (note.FingerIndex != null)
                                builder.Append("finger:")
                                    .Append((int)note.FingerIndex.Value)
                                    .Append(',');

                            if (note.IsImportant)
                                builder.Append("important: true,");

                            builder.Append('}');
                            break;
                    }

                    builder.Append(',');
                }


                builder.Append(']');

                fingering = builder.ToString();
            }

            return this.InvokeRenderMethodReturnBoundingBox("drawChord", x, y, name, new JsonString(fingering));
        }

        public async Task<Rect> DrawEllipseAroundNotes(Rect bounds)
        {
            var builder = new StringBuilder();
            builder.Append($"renderer.drawEllipseAroundBounds({{ left: {bounds.X}, top: {bounds.Y}, width: {bounds.Width}, height: {bounds.Height} }});");
            
            Debug.WriteLine(builder.ToString());

            var task = await this.BrowserMainFrame.EvaluateScriptAsync(builder.ToString());
            var result = (Dictionary<string, object>)task.Result;
            return PrimitiveRenderer.CreateBoundingBox(result);
        }
    }
}
