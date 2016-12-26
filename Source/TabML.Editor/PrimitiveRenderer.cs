﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Rendering;
using Rect = System.Windows.Rect;

namespace TabML.Editor
{
    class PrimitiveRenderer
    {

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

            BrowserContext.CallbackObject.Release();

            var result = BrowserContext.CallbackObject.Result;
            return PrimitiveRenderer.CreateBoundingBox(result);
        }

        private static Rect CreateBoundingBox(Dictionary<string, object> result)
        {
            return new Rect(Convert.ToDouble(result["left"]), Convert.ToDouble(result["top"]),
                            Convert.ToDouble(result["width"]), Convert.ToDouble(result["height"]));
        }

        private static string FormatArg(object arg)
        {
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
        public Task<Rect> DrawNoteFretting(string fretting, double x, double y, NoteRenderingFlags flags) 
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawNoteFretting", fretting, x, y, flags);
        public void DrawHorizontalBarLine(double x, double y, double length) 
            => this.InvokeRenderMethod("drawHorizontalBarLine", x, y, length);
        public void DrawBarLine(BarLine line, double x, double y) 
            => this.InvokeRenderMethod("drawBarLine", (int)line, x, y);
        public void DrawStem(double x, double yFrom, double yTo) 
            => this.InvokeRenderMethod("drawStem", x, yFrom, yTo);
        public Task<Rect> DrawFlag(BaseNoteValue noteValue, double x, double y, OffBarDirection offBarDirection) 
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawFlag", (int)noteValue, x, y, (int)offBarDirection);
        public void DrawBeam(double x1, double y1, double x2, double y2)
            => this.InvokeRenderMethod("drawBeam", x1, y1, x2, y2);
        public void DrawNoteValueAugment(NoteValueAugment augment, double x, double y) 
            => this.InvokeRenderMethod("drawNoteValueAugment", (int)augment, x, y);
        public void DrawRest(BaseNoteValue noteValue, double x, double y) 
            => this.InvokeRenderMethod("drawRest", (int)noteValue, x, y);
        public Task<Rect> MeasureRest(BaseNoteValue noteValue)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("measureRest", noteValue);
        public Task<Rect> DrawTuplet(string tuplet, double x, double y) 
            => this.InvokeRenderMethodReturnBoundingBox("drawTuplet", tuplet, x, y);
        public void DrawTie(double x0, double x1, double y, OffBarDirection offBarDirection)
            => this.InvokeRenderMethod("drawTie", x0, x1, y, (int)offBarDirection);
        public Task<Rect> DrawGliss(double x, double y, GlissDirection direction)
            => this.InvokeAsyncRenderMethodReturnBoundingBox("drawGliss", x, y, direction);
        public Task<Rect> DrawConnectionInstruction(double x, double y, string instruction, OffBarDirection offBarDirection)
            => this.InvokeRenderMethodReturnBoundingBox("drawConnectionInstruction", x, y, instruction, offBarDirection);
        public Task<Rect> DrawArtificialHarmonicText(double x, double y, string text, OffBarDirection offBarDirection)
            => this.InvokeRenderMethodReturnBoundingBox("drawArtificialHarmonicText", x, y, text, offBarDirection);
        public void Clear() => this.InvokeRenderMethod("clear");
    }
}
