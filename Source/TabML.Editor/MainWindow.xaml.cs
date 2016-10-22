using System.IO;
using System.Windows;
using TabML.Core;
using TabML.Editor.Tablature;
using TabML.Parser;
using TabML.Parser.Document;
using Bar = TabML.Editor.Tablature.Bar;

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

            var tablature = TabMLParser.TryParse(File.ReadAllText(@"..\..\..\..\Documentations\samples\my home town.txt"));

            foreach (var bar in tablature.Bars)
                this.BarStack.Children.Add(new Bar(bar));
        }
    }
}
