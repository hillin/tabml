using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Parser.Document
{
    public class Alternation : Element
    {
        public int[] Indices { get; set; }
        public AlternationTextType TextType { get; set; }
        public Explicity Explicity { get; set; }
    }
}
