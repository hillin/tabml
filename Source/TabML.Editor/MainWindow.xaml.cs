using System.IO;
using System.Windows;
using TabML.Core;
using TabML.Editor.Rendering;
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
        private bool _rendered = false;
        private bool _fakeLoaded = true;
        public MainWindow()
        {
            this.InitializeComponent();
            this.Browser.BrowserSettings.FileAccessFromFileUrls = CefSharp.CefState.Enabled;
            this.Browser.Address = Path.GetFullPath("../../../../TabMLWebRenderer/index.html");
        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
                return;

            if (_fakeLoaded)
            {
                _fakeLoaded = false;
                return;
            }

            if (!_rendered)
            {
                this.RenderTablature();
            }
        }

        private void RenderTablature()
        {
            var primitiveRenderer = new PrimitiveRenderer(this.Browser);
            var tablature = TabMLParser.TryParse(File.ReadAllText(@"..\..\..\..\..\Documentations\samples\bartest.txt"));
            var style = new TablatureStyle();
            var location = new Point(style.Padding.Left, style.Padding.Top);
            var size = new Size(
                800 - style.Padding.Left - style.Padding.Right,
                1200 - style.Padding.Top - style.Padding.Bottom);

            new TablatureRenderer(primitiveRenderer, style).Render(tablature, location, size);

            _rendered = true;
        }

        private void Browser_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {

        }

        private void Refresh_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.RenderTablature();
        }
    }
}
