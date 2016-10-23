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
using TabML.Editor.Tablature.Layout;
using DocumentBar = TabML.Parser.Document.Bar;

namespace TabML.Editor.Tablature
{
    public partial class Bar : IBarDrawingContext
    {
        private readonly ArrangedBar _arrangedBar;

        private readonly TablatureStyle _style;

        private readonly StackPanel[] _barLines;

        TablatureStyle IBarDrawingContext.Style => _style;

        public Bar(DocumentBar bar)
        {
            this.InitializeComponent();

            _style = new TablatureStyle();

            this.Resources["BarLineHeight"] = _style.BarLineHeight;

            _barLines = this.CreateBarLines();

            _arrangedBar = new BarArranger().Arrange(bar);

            _arrangedBar.Draw(this, 200);

        }

        private StackPanel[] CreateBarLines()
        {
            var barLines = new StackPanel[Defaults.Strings];
            for (var i = 0; i < Defaults.Strings; ++i)
            {
                barLines[i] = new StackPanel
                {
                    Style = (Style)BarLineStack.FindResource("BarLine") 
                };
                this.BarLineStack.Children.Add(barLines[i]);
            }

            return barLines;
        }
    }
}
