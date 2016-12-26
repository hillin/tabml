using System.IO;
using System.Threading.Tasks;
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
            BrowserContext.RegisterCallbackObject(this.Browser);
        }

        private async void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
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
                await this.RenderTablature();
            }
        }

        private async Task RenderTablature()
        {
            var primitiveRenderer = new PrimitiveRenderer(this.Browser);
            //var tablature = TabMLParser.TryParse(File.ReadAllText(@"..\..\..\..\..\Documentations\samples\temptest.txt"));
            //var tablature = TabMLParser.TryParse(File.ReadAllText(@"..\..\..\..\..\Documentations\samples\my home town.txt"));
            var tablature = TabMLParser.TryParse(File.ReadAllText(@"..\..\..\..\..\Documentations\samples\bartest.txt"));
            var style = new TablatureStyle();
            var location = new Point(style.Padding.Left, style.Padding.Top);
            var size = new Size(
                1200 - style.Padding.Left - style.Padding.Right,
                1600 - style.Padding.Top - style.Padding.Bottom);

            var renderingContext = new RenderingContext();

            var renderer = new TablatureRenderer(primitiveRenderer, style, tablature);
            renderer.Initialize();
            renderingContext.AssignRenderingContext(renderer, renderingContext);
            await renderer.Render(renderingContext, location, size);

            _rendered = true;
        }

        private void Browser_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {

        }

        private async void Refresh_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            await this.RenderTablature();
        }
    }
}
