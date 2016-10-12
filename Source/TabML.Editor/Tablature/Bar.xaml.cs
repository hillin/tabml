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
using DocumentBar = TabML.Parser.Document.Bar;

namespace TabML.Editor.Tablature
{
    public partial class Bar
    {
        private readonly DocumentBar _bar;

        public Bar(DocumentBar bar)
        {
            this.InitializeComponent();
            _bar = bar;
        }
    }
}
