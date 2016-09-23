using System.IO;
using System.Windows;
using TabML.Parser;

namespace TabML.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TabMLParser.TryParse(File.ReadAllText(@"E:\Documents\Guitar\hg-syntax.txt"));
        }
    }
}
