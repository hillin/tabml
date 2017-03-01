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
using TabML.Core.Style;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature
{

    public partial class ChordDiagramGridRow
    {
        private readonly ChordDefinition _chordDefinition;
        private readonly int _fret;
        private readonly bool _isCompact;

        private readonly ChordBarreStyle _barreStyle;

        public bool HasBraceBarre { get; private set; }

        public ChordDiagramGridRow(ChordDefinition chordDefinition, int fret, bool isCompact = false, ChordBarreStyle barreStyle = ChordBarreStyle.Brace)
        {
            _barreStyle = barreStyle;
            this.InitializeComponent();
            _chordDefinition = chordDefinition;
            _fret = fret;
            _isCompact = isCompact;

            this.Draw();
        }

        private void Draw()
        {
            this.DrawGrid();

            var notes = _chordDefinition.Fingering.Notes;
            LeftHandFingerIndex? fingerIndex = null;
            int? barreFrom = null;
            int? barreTo = null;
            for (var stringIndex = 0; stringIndex < notes.Length; ++stringIndex)
            {
                var note = notes[stringIndex];
                if (note.Fret != _fret)
                    continue;

                if (note.FingerIndex == null && fingerIndex == null)
                {
                    this.DrawSingleFingering(stringIndex);
                    continue;
                }

                if (_barreStyle == ChordBarreStyle.Brace)
                    this.DrawSingleFingering(stringIndex);

                if (note.FingerIndex != null)
                {
                    if (fingerIndex == null)
                        barreFrom = barreTo = stringIndex;
                    else
                    {
                        if (fingerIndex.Value == note.FingerIndex.Value)
                            barreTo = stringIndex;
                        else
                        {
                            this.DrawBarre(barreFrom.Value, barreTo.Value);

                            barreFrom = barreTo = stringIndex;
                        }
                    }

                    fingerIndex = note.FingerIndex;
                }
                else
                {
                    if (fingerIndex != null)
                    {
                        this.DrawBarre(barreFrom.Value, barreTo.Value);

                        barreFrom = barreTo = null;
                        fingerIndex = null;
                    }
                    else
                    {
                        this.DrawSingleFingering(stringIndex);
                    }
                }
            }

            if (fingerIndex != null)
                this.DrawBarre(barreFrom.Value, barreTo.Value);
        }

        private void DrawGrid()
        {
            if (_isCompact)
            {
                this.GridPath.Data = (Geometry)this.FindResource(_fret == 1
                    ? "ChordDiagramGridCompactOpenRow"
                    : "ChordDiagramGridCompactRow");
            }
            else
            {
                this.GridPath.Data = (Geometry)this.FindResource(_fret == 1
                    ? "ChordDiagramGridOpenRow"
                    : "ChordDiagramGridRow");
            }
        }

        private void DrawBarre(int barreFrom, int barreTo)
        {
            if (_barreStyle == ChordBarreStyle.Lined)
                this.DrawBarreFingering(barreFrom, barreTo);
            else
                this.DrawBraceBarre(barreFrom, barreTo);
        }

        private void DrawBraceBarre(int from, int to)
        {
            if (from == to)
                return;

            var length = to - from + 1;

            var path = new Path
            {
                Style = (Style)this.FindResource("BraceBarreStyle"),
                Data = this.GetBraceBarre(length)
            };

            Grid.SetColumn(path, from);
            Grid.SetColumnSpan(path, length);
            this.BraceBarreGrid.Children.Add(path);
            this.HasBraceBarre = true;
        }

        private Geometry GetBraceBarre(int length)
        {
            switch (length)
            {
                case 2:
                    return (Geometry)this.FindResource("ChordDiagramBarre2");
                case 3:
                    return (Geometry)this.FindResource("ChordDiagramBarre3");
                case 4:
                    return (Geometry)this.FindResource("ChordDiagramBarre4");
                case 5:
                    return (Geometry)this.FindResource("ChordDiagramBarre5");
                case 6:
                    return (Geometry)this.FindResource("ChordDiagramBarre6");
                default:
                    throw new ArgumentOutOfRangeException(nameof(length));
            }
        }

        private void DrawBarreFingering(int from, int to)
        {
            if (from == to)
            {
                this.DrawSingleFingering(from);
                return;
            }

            var length = to - from + 1;

            var path = new Path
            {
                Style = (Style)this.FindResource("FingeringTokenStyle"),
                Data = this.GetBarreFingeringGeometry(length)
            };

            Grid.SetColumn(path, from);
            Grid.SetColumnSpan(path, length);
            this.FingeringGrid.Children.Add(path);
        }

        private Geometry GetBarreFingeringGeometry(int length)
        {
            switch (length)
            {
                case 2:
                    return (Geometry)this.FindResource("ChordDiagramFingerBarrel2Filled");
                case 3:
                    return (Geometry)this.FindResource("ChordDiagramFingerBarrel3Filled");
                case 4:
                    return (Geometry)this.FindResource("ChordDiagramFingerBarrel4Filled");
                case 5:
                    return (Geometry)this.FindResource("ChordDiagramFingerBarrel5Filled");
                case 6:
                    return (Geometry)this.FindResource("ChordDiagramFingerBarrel6Filled");
                default:
                    throw new ArgumentOutOfRangeException(nameof(length));
            }
        }

        private void DrawSingleFingering(int stringIndex)
        {
            var path = new Path
            {
                Style = (Style)this.FindResource("FingeringTokenStyle"),
                Data = (Geometry)this.FindResource("ChordDiagramFingerFilled")
            };

            Grid.SetColumn(path, stringIndex);
            this.FingeringGrid.Children.Add(path);
        }
    }
}
