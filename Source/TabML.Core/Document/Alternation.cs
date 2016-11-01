using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Alternation : Element
    {
        public int[] Indices { get; set; }
        public AlternationTextType TextType { get; set; }
        public Explicity Explicity { get; set; }
    }
}
