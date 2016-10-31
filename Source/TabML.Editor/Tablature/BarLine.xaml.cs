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

namespace TabML.Editor.Tablature
{
    partial class BarLine : UserControl
    {
        private readonly TablatureStyle _style;

        public BarLine(TablatureStyle style)
        {
            this.InitializeComponent();
            _style = style;

            this.Resources["BarLineHeight"] = _style.BarLineHeight;
        }
    }
}
