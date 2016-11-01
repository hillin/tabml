using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TabML.Core;
using TabML.Core.Document;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature
{
    public partial class ChordDiagram
    {
        private readonly ChordDefinition _chordDefinition;
        private readonly ChordBarreStyle _barreStyle;

        public ChordDiagram(ChordDefinition chordDefinition, ChordBarreStyle barreStyle)
        {
            this.InitializeComponent();
            _chordDefinition = chordDefinition;
            _barreStyle = barreStyle;
            this.Draw();
        }

        private void Draw()
        {
            this.DrawChordName();

            var frets = _chordDefinition.Fingering.Notes.Where(n => n.Fret > 0).Select(n => n.Fret).ToArray();

            if (frets.Length == 0)
            {
                this.DrawSpecialStringTokens();

                for (var fret = 1; fret <= 3; ++fret)
                    this.DrawRow(fret, false, fret == 1);

                return;
            }

            var minFret = frets.Min();
            var maxFret = frets.Max();
            var fretSpan = maxFret - minFret + 1;

            if (fretSpan < 3)
            {
                if (maxFret < 3)
                {
                    maxFret = 3;
                    minFret = maxFret - 2;
                }
                else if (maxFret <= 4)
                    minFret = 1;
                else
                    minFret = maxFret - 2;
            }

            this.DrawSpecialStringTokens();

            if (minFret != 1)
                this.DrawFretOffset(minFret);

            var useCompact = fretSpan > 3;
            for (var fret = minFret; fret <= maxFret; ++fret)
                this.DrawRow(fret, useCompact, fret == minFret);

            this.DrawFingerIndices();
        }

        private void DrawChordName()
        {
            if (string.IsNullOrEmpty(_chordDefinition.GetDisplayName()))
                this.ChordNameText.Visibility = Visibility.Collapsed;
            else
            {
                this.ChordNameText.Visibility = Visibility.Visible;
                this.ChordNameText.Text = _chordDefinition.GetDisplayName();
            }
        }

        private void DrawFingerIndices()
        {
            var notes = _chordDefinition.Fingering.Notes;
            for (var stringIndex = 0; stringIndex < notes.Length; ++stringIndex)
            {
                if (notes[stringIndex].FingerIndex != null && notes[stringIndex].IsImportant)
                {
                    this.DrawFingerIndex(stringIndex, notes[stringIndex].FingerIndex.Value);
                }
            }
        }

        private void DrawFingerIndex(int stringIndex, LeftHandFingerIndex fingerIndex)
        {
            var text = new TextBlock
            {
                Text = fingerIndex.ToShortString(),
                Style = (Style)this.FindResource("ChordFingerIndexText")
            };

            Grid.SetColumn(text, stringIndex);

            this.FingeringGrid.Children.Add(text);
        }

        private void DrawSpecialStringTokens()
        {
            var notes = _chordDefinition.Fingering.Notes;
            for (var stringIndex = 0; stringIndex < notes.Length; ++stringIndex)
            {
                switch (notes[stringIndex].Fret)
                {
                    case Chord.FingeringSkipString:
                        this.DrawSkipStringToken(stringIndex);
                        break;
                    case 0:
                        this.DrawOpenStringToken(stringIndex);
                        break;
                }
            }
        }

        private void DrawOpenStringToken(int stringIndex)
        {
            var path = new Path
            {
                Style = (Style)this.FindResource("SpecialStringTokenStyle"),
                Data = (Geometry)this.FindResource("ChordDiagramOpenString")
            };

            Grid.SetColumn(path, stringIndex);
            this.SpecialStringsGrid.Children.Add(path);
        }

        private void DrawSkipStringToken(int stringIndex)
        {
            var path = new Path
            {
                Style = (Style)this.FindResource("SpecialStringTokenStyle"),
                Data = (Geometry)this.FindResource("ChordDiagramSkipString")
            };

            Grid.SetColumn(path, stringIndex);
            this.SpecialStringsGrid.Children.Add(path);
        }

        private void DrawRow(int fret, bool isCompact, bool isFirstRow)
        {
            var row = new ChordDiagramGridRow(_chordDefinition, fret, isCompact, _barreStyle);

            if (isFirstRow)
                this.BraceBarrePlaceholder.Visibility = row.HasBraceBarre ? Visibility.Visible : Visibility.Collapsed;

            this.DiagramGridPanel.Children.Add(row);
        }

        private void DrawFretOffset(int minFret)
        {
            this.FretOffsetContainer.Visibility = Visibility.Visible;
            this.FretOffsetText.Text = minFret.ToString();
        }
    }
}
