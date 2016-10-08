namespace TabML.Parser.Document
{
    class Chord : Element
    {
        public const int FingeringSkipString = -1;

        public string Name { get; set; }
        public int[] Fingering { get; set; }
        
    }
}
