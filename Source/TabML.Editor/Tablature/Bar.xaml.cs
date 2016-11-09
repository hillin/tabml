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
using DocumentBar = TabML.Core.Document.Bar;

namespace TabML.Editor.Tablature
{
    partial class Bar 
    {
        private readonly ArrangedBar _arrangedBar;

        private readonly TablatureStyle _style;

        private readonly BarLine[] _barLines;
        

        public Bar(DocumentBar bar)
        {
            this.InitializeComponent();

            _style = new TablatureStyle();

            this.Resources["BarLineHeight"] = _style.BarLineHeight;

            _barLines = this.CreateBarLines();

            _arrangedBar = new BarArranger().Arrange(bar);

            //_arrangedBar.DrawHead(this, 200);

        }

        private BarLine[] CreateBarLines()
        {
            var barLines = new BarLine[Defaults.Strings];
            for (var i = 0; i < Defaults.Strings; ++i)
            {
                barLines[i] = new BarLine(_style);
                this.BarLineStack.Children.Add(barLines[i]);
            }

            return barLines;
        }
    }
}
