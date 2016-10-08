using System;

namespace TabML.Parser.Document
{
    class Chord : Element
    {
        public const int FingeringSkipString = -1;

        public string Name { get; set; }
        public int[] Fingering { get; set; }

        public Chord Clone()
        {
            return new Chord
            {
                Name = this.Name,
                Fingering = (int[])this.Fingering?.Clone()
            };
        }
    }
}
