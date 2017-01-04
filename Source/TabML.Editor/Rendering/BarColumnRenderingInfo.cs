using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    class BarColumnRenderingInfo
    {
        private bool[] _occupiedStrings;
        public BarColumn Column { get; }
        public double Position { get; }
        public double Width { get; }

        public Rect?[] NoteBoundingBoxes { get; }

        public BarColumnRenderingInfo(BarColumn column, double position, double width)
        {
            this.Column = column;
            this.Position = position;
            this.Width = width;
            this.NoteBoundingBoxes = new Rect?[Defaults.Strings];
        }

        public bool MatchesChord
        {
            get
            {
                if (this.Column.Chord?.Fingering == null)
                    return false;

                return this.Column.VoiceBeats.All(b => b.Notes.All(n => n.MatchesChord(this.Column.Chord.Fingering)));
            }
        }

        public bool HasBrushlikeTechnique
        {
            get
            {
                if (this.Column.VoiceBeats.Count == 0)
                    return false;
                return this.Column.VoiceBeats.Any(b => b.StrumTechnique == StrumTechnique.ArpeggioDown
                                                       || b.StrumTechnique == StrumTechnique.ArpeggioUp
                                                       || b.StrumTechnique == StrumTechnique.BrushDown
                                                       || b.StrumTechnique == StrumTechnique.BrushUp
                                                       || b.StrumTechnique == StrumTechnique.Rasgueado);
            }
        }

        public double GetNoteAlternationOffsetRatio(int stringIndex)
        {
            if (_occupiedStrings == null)
            {
                _occupiedStrings = new bool[Defaults.Strings];
                foreach (var beat in this.Column.VoiceBeats)
                {
                    var targetBeat = beat.IsTied ? beat.GetTieHead() : beat;
                    if (targetBeat == null || targetBeat.IsRest)
                        continue;

                    foreach (var i in targetBeat.Notes.Select(n => n.String))
                        _occupiedStrings[i] = true;
                }
            }

            if (stringIndex == 0)
                return _occupiedStrings[1] ? -0.25 : 0;

            var continuousStringsBefore = 0;
            for (var i = stringIndex - 1; i >= 0; --i)
            {
                if (_occupiedStrings[i])
                    ++continuousStringsBefore;
                else
                    break;
            }

            if (continuousStringsBefore == 0)
            {
                if (stringIndex == Defaults.Strings - 1)
                    return 0;

                if (_occupiedStrings[stringIndex + 1])
                    return -0.25;

                return 0;
            }

            return (continuousStringsBefore % 2 - 0.5) / 2;
        }

    }
}
