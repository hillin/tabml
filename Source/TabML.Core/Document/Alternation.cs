using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Alternation : Element
    {
        public int[] Indices { get; set; }
        public AlternationTextType TextType { get; set; }
        public Explicity Explicity { get; set; }
        public override IEnumerable<Element> Children { get { yield break; } }

        public string GetFormattedIndices()
        {
            switch (this.TextType)
            {
                case AlternationTextType.Arabic:
                    return string.Join(" ", this.Indices.Select(i => $"{i}."));
                case AlternationTextType.RomanUpper:
                    return string.Join(" ", this.Indices.Select(i => $"{i.ToRoman()}."));
                case AlternationTextType.RomanLower:
                    return string.Join(" ", this.Indices.Select(i => $"{i.ToRoman().ToLower()}."));
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
