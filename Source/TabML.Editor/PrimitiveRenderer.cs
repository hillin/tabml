using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.Wpf;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Rendering;

namespace TabML.Editor
{
    class PrimitiveRenderer
    {
        private readonly ChromiumWebBrowser _browser;

        public PrimitiveRenderer(ChromiumWebBrowser browser)
        {
            _browser = browser;
        }

        private void InvokeScriptAsync(string script)
        {
            _browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(script);
        }

        private void InvokeRenderMethod(string method, params object[] args)
        {
            var builder = new StringBuilder();
            builder.Append("renderer.")
                   .Append(method)
                   .Append("(")
                   .Append(string.Join(", ", args.Select(PrimitiveRenderer.Stringify)))
                   .Append(");");

            this.InvokeScriptAsync(builder.ToString());
        }

        private static string Stringify(object arg)
        {
            if (arg is string)
                return $"\"{arg}\"";

            return arg.ToString();
        }

        public void DrawTitle(string title, double x, double y) => this.InvokeRenderMethod("drawTitle", title, x, y);
        public void DrawLyrics(string lyrics, double x, double y) => this.InvokeRenderMethod("drawLyrics", lyrics, x, y);
        public void DrawFretNumber(string fretNumber, double x, double y) => this.InvokeRenderMethod("drawFretNumber", fretNumber, x, y);
        public void DrawHorizontalBarLine(double x, double y, double length) => this.InvokeRenderMethod("drawHorizontalBarLine", x, y, length);
        public void DrawBarLine(BarLine line, double x, double y) => this.InvokeRenderMethod("drawBarLine", (int)line, x, y);
        public void DrawStem(double x, double yFrom, double yTo) => this.InvokeRenderMethod("drawStem", x, yFrom, yTo);
        public void DrawFlag(BaseNoteValue noteValue, double x, double y, OffBarDirection offBarDirection) => this.InvokeRenderMethod("drawFlag", (int)noteValue, x, y, (int)offBarDirection);
        public void DrawBeam(double x1, double y1, double x2, double y2)
            => this.InvokeRenderMethod("drawBeam", x1, y1, x2, y2);
    }
}
