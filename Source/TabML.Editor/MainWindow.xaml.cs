using System.IO;
using System.Windows;
using TabML.Core;
using TabML.Editor.Tablature;
using TabML.Parser;
using TabML.Parser.Document;

namespace TabML.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            TabMLParser.TryParse(File.ReadAllText(@"..\..\..\..\Documentations\samples\hg-syntax.txt"));

        }
    }
}
