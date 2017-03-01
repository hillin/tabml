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
using TabML.Core.Player;
using TabML.Core.Style;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Test
{
    public partial class ChordTest : UserControl
    {
        public ChordTest()
        {
            this.InitializeComponent();

            this.DrawTestChords();
        }

        private void DrawTestChords()
        {
            this.DrawFChord();
            this.DrawCmaj7OnGChord();
            this.DrawEmOnDChord();
        }

        private void DrawEmOnDChord()
        {
            var chord = new ChordDefinition()
            {
                DisplayName = "Em/D",
                Name = "Em/D",
                Fingering = new ChordFingering
                {
                    Notes = new[]
                    {
                        new ChordFingeringNote (-1),
                        new ChordFingeringNote (-1),
                        new ChordFingeringNote (0),
                        new ChordFingeringNote (0),
                        new ChordFingeringNote (0),
                        new ChordFingeringNote (0),
                    }
                }
            };

            this.Root.Children.Add(new ChordDiagram(chord, ChordBarreStyle.Lined));
        }

        private void DrawCmaj7OnGChord()
        {
            var chord = new ChordDefinition()
            {
                DisplayName = "Cmaj7/G",
                Name = "Cmaj7/G",
                Fingering = new ChordFingering
                {
                    Notes = new[]
                    {
                        new ChordFingeringNote (3, LeftHandFingerIndex.Index, true),
                        new ChordFingeringNote (3, LeftHandFingerIndex.Index, true),
                        new ChordFingeringNote (5, LeftHandFingerIndex.Ring, true),
                        new ChordFingeringNote (5, LeftHandFingerIndex.Ring, true),
                        new ChordFingeringNote (5, LeftHandFingerIndex.Ring, true),
                        new ChordFingeringNote (7, LeftHandFingerIndex.Pinky, true),
                    }
                }
            };

            this.Root.Children.Add(new ChordDiagram(chord, ChordBarreStyle.Lined));
        }

        private void DrawFChord()
        {
            var chord = new ChordDefinition()
            {
                DisplayName = "F",
                Name = "F",
                Fingering = new ChordFingering
                {
                    Notes = new[]
                    {
                        new ChordFingeringNote(1, LeftHandFingerIndex.Index),
                        new ChordFingeringNote(3, LeftHandFingerIndex.Ring, true),
                        new ChordFingeringNote(3, LeftHandFingerIndex.Pinky, true),
                        new ChordFingeringNote(2, LeftHandFingerIndex.Middle, true),
                        new ChordFingeringNote(1, LeftHandFingerIndex.Index),
                        new ChordFingeringNote(1, LeftHandFingerIndex.Index),
                    }
                }
            };

            this.Root.Children.Add(new ChordDiagram(chord, ChordBarreStyle.Brace));
        }
    }
}
