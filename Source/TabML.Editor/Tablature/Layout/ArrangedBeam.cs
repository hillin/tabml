﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    [DebuggerDisplay("{DebugString, nq}")]
    [DebuggerTypeProxy(typeof(DebugView))]
    class ArrangedBeam : IBeatElement
    {
        private class DebugView
        {
            private readonly ArrangedBeam _beam;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            // ReSharper disable once UnusedMember.Local
            public IBeatElement[] Elements => _beam.Elements.ToArray();

            public DebugView(ArrangedBeam beam)
            {
                _beam = beam;
            }
        }

        public BaseNoteValue BeatNoteValue { get; internal set; }
        public VoicePart VoicePart { get; }
        public bool IsRoot { get; }
        public List<IBeatElement> Elements { get; }
        public int? Tuplet { get; set; }

        public ArrangedBeam(BaseNoteValue beatNoteValue, VoicePart voicePart, bool isRoot)
        {
            this.BeatNoteValue = beatNoteValue;
            this.VoicePart = voicePart;
            this.IsRoot = isRoot;
            this.Elements = new List<IBeatElement>();
        }

        public PreciseDuration GetDuration()
        {
            return this.Elements.Sum(b => b.GetDuration());
        }

        public bool GetIsTupletFull()
        {
            if (this.Tuplet == null)
                return false;

            return this.BeatNoteValue.GetDuration() * this.Tuplet.Value / 2 <= this.GetDuration();
        }

        public ArrangedBeat FirstBeat
        {
            get
            {
                if (this.Elements.Count == 0)
                    return null;
                var beat = this.Elements[0] as ArrangedBeat;
                return beat ?? ((ArrangedBeam)this.Elements[0]).FirstBeat;
            }
        }

        public ArrangedBeat LastBeat
        {
            get
            {
                if (this.Elements.Count == 0)
                    return null;
                var beat = this.Elements[this.Elements.Count - 1] as ArrangedBeat;
                return beat ?? ((ArrangedBeam)this.Elements[this.Elements.Count - 1]).LastBeat;
            }
        }

        public bool MatchesTuplet(ArrangedBeat beat)
        {
            return this.BeatNoteValue > beat.Beat.NoteValue.Base    // if we are large enough to create a child beam for this beat
                || this.Tuplet == beat.Beat.NoteValue.Tuplet;       // or our tuplet exactly matches
        }

#if DEBUG
        private string DebugString
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("Beam of ");
                builder.Append(this.BeatNoteValue);

                if (this.Tuplet != null)
                    builder.Append('/').Append(this.Tuplet);

                builder.Append('(').Append(this.GetDuration()).Append(')');

                return builder.ToString();
            }
        }
#endif

        public void Draw(IBarDrawingContext drawingContext, double[] columnPositions, BeamSlope beamSlope)
        {
            var firstBeat = this.FirstBeat;
            var lastBeat = this.LastBeat;

            var x0 = columnPositions[firstBeat.Column.ColumnIndex];
            var x1 = columnPositions[lastBeat.Column.ColumnIndex];

            if (beamSlope == null)
            {
                var y0 = firstBeat.GetStemTailPosition(drawingContext);
                var y1 = lastBeat.GetStemTailPosition(drawingContext);
                beamSlope = new BeamSlope(x0, y0, (y1 - y0) / (x1 - x0));
            }

            foreach (var element in this.Elements)
                element.Draw(drawingContext, columnPositions, beamSlope);

            drawingContext.DrawBeam(this.BeatNoteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), this.VoicePart);
            if (this.Tuplet != null
                && this.Elements.Any(e => (e as ArrangedBeam)?.Tuplet != this.Tuplet))
            {
                var tupletX = (x1 + x0) / 2;
                drawingContext.DrawTuplet(this.Tuplet.Value, tupletX, beamSlope.GetY(tupletX), this.VoicePart);
            }
        }
    }
}
