using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                   .Append(string.Join(", ", args.Select(PrimitiveRenderer.FormatArg)))
                   .Append(");");

            Debug.WriteLine(builder.ToString());
            this.InvokeScriptAsync(builder.ToString());
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

        public void DrawTitle(string title, double x, double y) => this.InvokeRenderMethod("drawTitle", title, x, y);
        public void DrawLyrics(string lyrics, double x, double y) => this.InvokeRenderMethod("drawLyrics", lyrics, x, y);
        public void DrawFretNumber(string fretNumber, double x, double y, bool isHalfOrLonger) => this.InvokeRenderMethod("drawFretNumber", fretNumber, x, y, isHalfOrLonger);
        public void DrawDeadNote(double x, double y, bool isHalfOrLonger) => this.InvokeRenderMethod("drawDeadNote", x, y, isHalfOrLonger);
        public void DrawPlayToChordMark(double x, double y, bool isHalfOrLonger) => this.InvokeRenderMethod("drawPlayToChordMark", x, y, isHalfOrLonger);
        public void DrawHorizontalBarLine(double x, double y, double length) => this.InvokeRenderMethod("drawHorizontalBarLine", x, y, length);
        public void DrawBarLine(BarLine line, double x, double y) => this.InvokeRenderMethod("drawBarLine", (int)line, x, y);
        public void DrawStem(double x, double yFrom, double yTo) => this.InvokeRenderMethod("drawStem", x, yFrom, yTo);
        public void DrawFlag(BaseNoteValue noteValue, double x, double y, OffBarDirection offBarDirection) => this.InvokeRenderMethod("drawFlag", (int)noteValue, x, y, (int)offBarDirection);
        public void DrawBeam(double x1, double y1, double x2, double y2) => this.InvokeRenderMethod("drawBeam", x1, y1, x2, y2);
        public void DrawNoteValueAugment(NoteValueAugment augment, double x, double y) => this.InvokeRenderMethod("drawNoteValueAugment", (int)augment, x, y);
        public void DrawRest(BaseNoteValue noteValue, double x, double y) => this.InvokeRenderMethod("drawRest", (int)noteValue, x, y);
        public void DrawTuplet(string tuplet, double x, double y) => this.InvokeRenderMethod("drawTuplet", tuplet, x, y);

        public void DrawTie(double x0, double x1, double y, OffBarDirection offBarDirection)
            => this.InvokeRenderMethod("drawTie", x0, x1, y, (int)offBarDirection);

        public void DrawGliss(double x, double y, GlissDirection direction)
            => this.InvokeRenderMethod("drawGliss", x, y, direction);

        public void DrawTieInstruction(double x, double y, string instruction)
            => this.InvokeRenderMethod("drawTieInstruction", x, y, instruction);

        public void Clear() => this.InvokeRenderMethod("clear");
    }
}
